using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec03LibN
{
    static public partial class Lec03BLib
    {
        static public IFactory getL1() {
            return new FactoryL1(); 
        }
        static public IFactory getL2(float a) {
            return new FactoryL2(a);
        }
        static public IFactory getL3(float a, float b)
        {
            return new FactoryL3(a, b);
        }
    }
}
