using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IExtensionRepository<TEntity> : IRepository<TEntity>, IOrderableRepository<TEntity> where TEntity : class 
    {
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="item"></param>
        void Insert(IEnumerable<TEntity> item);
        /// <summary>
        /// 更新集合
        /// </summary>
        /// <param name="item"></param>
        void Update(IEnumerable<TEntity> item);
        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="item"></param>
        void Delete(IEnumerable<TEntity> item);
        /// <summary>
        /// 此方法不能和GetModel()方法一同使用，表主键可以通过post和get方法获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        void Update<T>(Expression<Action<T>> item) where T : class;
        /// <summary>
        /// 根据lambda表达式，得到延时结果集
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据lambda表达式获取一个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 批量插入 插入之前是否清楚自增长属性，默认不清除
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isRemoveIdentity"></param>
        void BulkInsert(IEnumerable<TEntity> item, bool isRemoveIdentity);
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="item"></param>
        void BulkInsert(IEnumerable<TEntity> item);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldParams"></param>
        void BulkUpdate(IEnumerable<TEntity> item, params string[] fieldParams);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="item"></param>
        void BulkDelete(IEnumerable<TEntity> item);
    }
}
