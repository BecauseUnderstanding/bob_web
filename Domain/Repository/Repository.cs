using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;
using ESBasic.Loggers;

namespace Domain
{
    /// <summary>
    /// DbContext上下文仓储功能类，领域上下文可以直接继承它
    /// 生命周期：数据上下文的生命周期为一个HTTP请求的结束
    /// 相关说明：
    /// 1 领域对象使用声明IRepository和IExtensionRepository接口得到不同的操作规范
    /// 2 可以直接为上下注入Action<string>的委托实例，用来记录savechanges产生的异常
    /// 3 可以订阅BeforeSaved和AfterSaved两个事件，用来在方法提交前与提交后实现代码注入
    /// 4 所有领域db上下文都要继承iUnitWork接口，用来实现工作单元，这对于提升程序性能与为重要
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : ISpecificationRepository<TEntity> where TEntity : class
    {
        
        public Repository(IUnitOfWork db,FileAgileLogger logger)
        {
            UnitOfWork = db;
            Db = (DataBaseContext)db;
            FileAgileLogger = logger;
            ((IObjectContextAdapter)db).ObjectContext.CommandTimeout = 0;
        }

        public Repository(IUnitOfWork db)
            : this(db, null)
        {
        }

        #region Properties
        /// <summary>
        /// 数据上下文
        /// </summary>
        protected DataBaseContext Db { get; private set; }
        /// <summary>
        /// 工作单元上下文 子类可以直接使用它
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// 日志记录
        /// </summary>
        protected FileAgileLogger FileAgileLogger { get; set; }


        #endregion

        #region  Fields
        /// <summary>
        /// 数据总数
        /// </summary>
        int _dataTotalCount = 0;
        /// <summary>
        /// 数据页数
        /// </summary>
        int _dataTotalPages = 0;
        /// <summary>
        /// 数据页面大小
        /// </summary>
        private const int DataPageSizes = 10000;
        #endregion

        #region Parvite Methods

        private void DataPageProcess(IEnumerable<TEntity> item, Action<IEnumerable<TEntity>> method)
        {
            if (item != null && item.Any())
            {
                _dataTotalCount = item.Count();
                if (_dataTotalCount % DataPageSizes > 0)
                    _dataTotalCount += 1;
                for (int pageIndex = 1; pageIndex <= _dataTotalCount; pageIndex++)
                {
                    var currentItems = item.Skip((pageIndex - 1) * DataPageSizes).Take(DataPageSizes).ToList();
                    method(currentItems);
                }
            }
        }


        protected void SaveChanges()
        {
            try
            {
                Db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }



        private static string GetParamTag(int paramId)
        {
            return "{" + paramId + "}";
        }

        private static string GetEqualStatment(string fieldName, int paramId, Type pkType)
        {
            if (pkType.IsValueType)//判断是否为值类型
                return string.Format("{0}={1}", fieldName, GetParamTag(paramId));
            return string.Format("{0}='{1}'", fieldName, GetParamTag(paramId));
        }

        /// <summary>
        /// 得到实体键的EntityKey
        /// </summary>
        /// <returns></returns>
        //protected ReadOnlyMetadataCollection<EdmMember> GetPrimaryKey()
        //{
        //    EntitySetBase primaryKey = ((IObjectContextAdapter)Db).ObjectContext.GetEntitySet(typeof(TEntity));
        //    if (primaryKey == null)
        //        return null;
        //    ReadOnlyMetadataCollection<EdmMember> arr = primaryKey.ElementType.KeyMembers;
        //    return arr;
        //}



        #endregion


        public IQueryable<TEntity> GetModel(ISpecification<TEntity> specification)
        {
            return GetModel(specification);
        }

        public TEntity Find(ISpecification<TEntity> specification)
        {
            return GetModel(specification).FirstOrDefault();
        }

        public IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderby, ISpecification<TEntity> specification)
        {
            var linq = new Orderable<TEntity>(GetModel(specification));
            orderby(linq);
            return linq.Queryable;
        }

        //public event Action<EventArgs> AfterSaved;

        //public event Action<EventArgs> BeforSaved;

        public virtual void Insert(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i =>
            {
                Db.Entry<TEntity>(i);
                Db.Set<TEntity>().Add(i);
            });
            this.SaveChanges();
        }

        public virtual void Update(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i =>
            {
                Db.Entry<TEntity>(i);
                Db.Entry<TEntity>(i).State = EntityState.Modified;
            });
            try
            {
                SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                ((IObjectContextAdapter)Db).ObjectContext.Refresh(RefreshMode.ClientWins, item);
                SaveChanges();
            }
        }

        public virtual void Delete(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i =>
            {
                Db.Set<TEntity>().Attach(i);
                Db.Set<TEntity>().Remove(i);
            });
            this.SaveChanges();

        }

        public void Update<T>(System.Linq.Expressions.Expression<Action<T>> item) where T : class
        {
            T newTEntity = typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null) as T;
            List<string> propertyNameList = new List<string>();
            MemberInitExpression param = item.Body as MemberInitExpression;
            foreach (var property in param.Bindings)
            {
                string propertyName = property.Member.Name;
                object propertyValue;
                var meberAssignment = property as MemberAssignment;
                if (meberAssignment.Expression.NodeType == ExpressionType.Constant)
                {
                    propertyValue = (meberAssignment.Expression as ConstantExpression).Value;
                }
                else
                {
                    propertyValue = Expression.Lambda(meberAssignment.Expression, null).Compile().DynamicInvoke();
                }
                typeof(T).GetProperty(propertyName).SetValue(newTEntity, propertyValue, null);
                propertyNameList.Add(propertyName);
            }
            try
            {
                Db.Set<T>().Attach(newTEntity);
            }
            catch (Exception e)
            {
                FileAgileLogger.Log(e, null, ErrorLevel.Standard);
                throw new Exception("本方法不能和GetModel()一起使用，请使用Update(TEntity entity)");
            }
            Db.Configuration.ValidateOnSaveEnabled = false;
            var ObjectStateEntry =
                ((IObjectContextAdapter)Db).ObjectContext.ObjectStateManager.GetObjectStateEntry(newTEntity);
            propertyNameList.ForEach(r => ObjectStateEntry.SetModifiedProperty(r.Trim()));
            try
            {
                SaveChanges();
            }
            catch (OptimisticConcurrencyException e)
            {
                FileAgileLogger.Log(e,null,ErrorLevel.Standard);
                ((IObjectContextAdapter)Db).ObjectContext.Refresh(RefreshMode.ClientWins, newTEntity);
                SaveChanges();
            }
        }

        public IQueryable<TEntity> GetModel(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return GetModel().Where(predicate);
        }

        public TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return GetModel(predicate).FirstOrDefault();
        }

        public void BulkInsert(IEnumerable<TEntity> item, bool isRemoveIdentity)
        {
            throw new NotImplementedException();
        }

        public void BulkInsert(IEnumerable<TEntity> item)
        {
            throw new NotImplementedException();
        }

        public void BulkUpdate(IEnumerable<TEntity> item, params string[] fieldParams)
        {
            throw new NotImplementedException();
        }

        public void BulkDelete(IEnumerable<TEntity> item)
        {
            throw new NotImplementedException();
        }

        public void SetDbContext(IUnitOfWork unitOfWork)
        {
            Db = (DataBaseContext)unitOfWork;
            UnitOfWork = unitOfWork;
        }

        public void Insert(TEntity item)
        {
            this.Db.Entry<TEntity>(item);
            this.Db.Set<TEntity>().Add(item);
            this.SaveChanges();
        }

        public void Update(TEntity item)
        {
            Db.Set<TEntity>().Attach(item);
            Db.Entry(item).State = EntityState.Modified;
            try
            {
                this.SaveChanges();
            } 
            catch (OptimisticConcurrencyException ex)//并发冲突异常
            {
                ((IObjectContextAdapter)Db).ObjectContext.Refresh(RefreshMode.ClientWins, item);
                this.SaveChanges();
            }
        }

        public void Delete(TEntity item)
        {
            Db.Entry<TEntity>(item);
            Db.Set<TEntity>().Remove(item);
            this.SaveChanges();
        }

        public virtual IQueryable<TEntity> GetModel()
        {
            return Db.Set<TEntity>();
        }

        public TEntity Find(params object[] id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderby)
        {
            var linq = new Orderable<TEntity>(GetModel());
            orderby(linq);
            return linq.Queryable;
        }

        public IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderby, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            var linq = new Orderable<TEntity>(GetModel(predicate));
            orderby(linq);
            return linq.Queryable;
        }




    }
}
