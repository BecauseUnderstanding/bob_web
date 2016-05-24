using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class SpecificationExtensions
    {
        public static Func<T, bool> And<T>(this Func<T,bool> left ,Func<T,bool> right  )
        {
            return candicate => left(candicate) && right(candicate);
        }

        public static Func<T, bool> Or<T>(this Func<T, bool> left, Func<T, bool> right)
        {
            return candicate => left(candicate) || right(candicate);
        }

        public static Func<T, bool> Not<T>(this Func<T, bool> one)
        {
            return candicate => !one(candicate);
        }


    }
}
