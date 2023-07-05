using System;
using System.Collections.Generic;
using System.Text;

namespace lab01
{
    internal class Class3
    {
        protected int a;
        public int B { get; set; }

        public void Method1()
        {
            Console.WriteLine("Метод Class3");
        }
        public Class3()
        {
            Console.WriteLine("Конструктор Class3");
        }

    }
}
