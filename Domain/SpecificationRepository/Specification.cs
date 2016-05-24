using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// 规约模式解耦了仓储操作和筛选条件，如果业务扩展 只需定制Sperification然后注入到仓储，仓储的接口和实现无需更改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  class Specification<T> : ISpecification<T> where T : class
    {
        private readonly Func<T, bool> _isSaticfiedBy;

        public Specification(Func<T,bool> isSaticfiedBy )
        {
            _isSaticfiedBy = isSaticfiedBy;
        }

        public bool IsSatisfiedBy(T item)
        {
            return _isSaticfiedBy(item);
        }


    }
}
