using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Orderable<T> : IOrderable<T>
    {
        private IQueryable<T> _queryable;

        /// <summary>
        /// 排序后的结果集
        /// </summary>
        /// <param name="queryable"></param>
        public Orderable(IQueryable<T> queryable)
        {
            _queryable = queryable;
        }


        /// <summary>
        /// 递增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> Asc<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>).OrderBy(keySelector);
            return this;
        }
        /// <summary>
        /// 批量递增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> ThenAsc<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>).ThenBy(keySelector);
            return this;
        }
        /// <summary>
        /// 递减
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> Desc<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>).OrderByDescending(keySelector);
            return this;
        }
        /// <summary>
        /// 批量递减
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<T> ThenDesc<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<T>).ThenByDescending(keySelector);
            return this;
        }

        public IQueryable<T> Queryable
        {
            get { return _queryable; }
        }
    }
}
