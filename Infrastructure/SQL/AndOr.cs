using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQL
{
    public class AndOr: Base
    {
        public AndOr(Base prev): base(prev) { }
        public Where Where(string column)
        {
            return new Where(column, this);
        } 
    }
}
