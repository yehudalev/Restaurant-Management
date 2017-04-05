using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalFactory
    {
        //public static Idal getDal(){return Dal_imp.Instance; } 
        public static Idal getDal() { return DalWithXml.Instance; }
    }
}
