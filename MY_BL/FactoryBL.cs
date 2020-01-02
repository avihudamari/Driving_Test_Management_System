using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MY_BL
{
    public class FactoryBL
    {
        protected static IBL instance = null;
        public static IBL GetInstance()
        {
            if (instance == null)
            {
                instance = new Bl_imp();
            }
            return instance;
        }
    }
}
