using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static msunsoft.service.calculator;

namespace thriftDemoServer
{
    public class Server : Iface
    {
        public int add(int arg1, int arg2)
        {
            return 3;
        }

        public int division(int arg1, int arg2)
        {
            return 4;
        }

        public int multiply(int arg1, int arg2)
        {
            throw new NotImplementedException();
        }

        public int subtract(int arg1, int arg2)
        {
            throw new NotImplementedException();
        }
    }
}
