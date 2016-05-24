using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        /// <summary>
        /// 设定全局的上下文
        /// </summary>
        /// <param name="unitOfWork"></param>
        void SetDbContext(IUnitOfWork unitOfWork);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="item"></param>
        void Insert(TEntity item);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="item"></param>
        void Update(TEntity item);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="item"></param>
        void Delete(TEntity item);
        /// <summary>
        /// 获得实体集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetModel();
        /// <summary>
        /// 根据主键id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(params object[] id);


    }
}
