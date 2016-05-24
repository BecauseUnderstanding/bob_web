using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// 基类DbContext全部继承
    /// </summary>
    public class DataBaseContext : DbContext
    {
        internal virtual DataBaseContext Master
        {
            get { throw new NotImplementedException(); }
        }

        public DataBaseContext(string conncetKey)
            : base(conncetKey)
        {

        }
    }
}
