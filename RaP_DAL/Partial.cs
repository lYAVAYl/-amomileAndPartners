using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaP_DAL
{
    public partial class User
    {
        public override string ToString()
        {
            return Login.Length > 100 ? Login.Substring(0, 100) + "..." : Login;
        }
    }

    public partial class Company
    {
        public override string ToString()
        {
            return Name.Length > 70 ? Name.Substring(0, 70)+"..." : Name;
        }
    }
}
