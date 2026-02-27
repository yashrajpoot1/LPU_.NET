using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class MyCustomException: Exception
    {
        public MyCustomException():base() 
        {

        }
        public MyCustomException(string msg) : base(msg)
        {

        }
    }
}
