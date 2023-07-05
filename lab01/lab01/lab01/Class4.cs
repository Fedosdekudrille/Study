using System;
using System.Collections.Generic;
using System.Text;

namespace lab01
{
    internal class Class4 : Class3
    {
        private int с;
        public int C
        {
            get { return с; }
            set { с = value; }
        }

        public Class4() : base()
        {
            Console.WriteLine("Конструктор Class4");
        }

        public new void Method1()
        {
            Console.WriteLine("Метод Class4");
        }

        public void Method2()
        {
            Console.WriteLine("Метод2");
        }
    }
}
