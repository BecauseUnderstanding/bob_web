using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ISpecification<T> where T : class
    {
        bool IsSatisfiedBy(T item);


    }
}
