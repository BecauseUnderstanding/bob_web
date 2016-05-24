using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ISpecificationRepository<TEntity> : IExtensionRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// 按照指定规约 得到延时结果集
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(ISpecification<TEntity> specification);

        /// <summary>
        /// 按照指定规约 得到一个实体
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        TEntity Find(ISpecification<TEntity> specification);

        /// <summary>
        /// 带排序功能 按照指定规约 得到延时结果集
        /// </summary>
        /// <param name="orderby"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderby, ISpecification<TEntity> specification);

        ///// <summary>
        ///// 保存之后执行
        ///// </summary>
        //event Action<EventArgs> AfterSaved;

        ///// <summary>
        ///// 保存之前执行
        ///// </summary>
        //event Action<EventArgs> BeforSaved;

    }
}
