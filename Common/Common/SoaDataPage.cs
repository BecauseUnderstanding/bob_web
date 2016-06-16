using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common
{
    /// <summary>
    /// 公用model
    /// </summary>
    public class SoaDataPage
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 升降序
        /// </summary>
        public string SortCol { get; set; }
    }

    /// <summary>
    /// 升降序枚举
    /// </summary>
    public enum Sort
    {
        [Display(Name = "升序")]
        Asc = 0,
        [Display(Name = "降序")]
        Desc = 1


    }

    /// <summary>
    /// 返回分页信息
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SoaDataPageResponse<TEntity>
    {
        /// <summary>
        /// 返回行数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public IEnumerable<TEntity> Body { get; set; }
    }

}


