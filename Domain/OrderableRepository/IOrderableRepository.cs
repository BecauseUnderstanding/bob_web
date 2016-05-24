using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IOrderableRepository<TEntity> where TEntity : class 
    {
        /// <summary>
        /// 带排序的结果集
        /// </summary>
        /// <param name="orderby"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderby);

        /// <summary>
        /// 根据lambda表达式和排序方式，得到延时结果集
        /// </summary>
        /// <param name="orderby"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderby, Expression<Func<TEntity, bool>> predicate);
    }
}
