using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MY_DAL
{
    public class FactoryDal
    {
        protected static Idal instance = null;
        public static Idal GetInstance()
        {
            if (instance == null)
            {
                instance = new Dal_XML_imp();
            }
            return instance;
        }
    }
}
