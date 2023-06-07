using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Errors
{
    public class AccessDeniedException: ClientException
    {
        public AccessDeniedException() : base("Access Denied") { }
    }
}
