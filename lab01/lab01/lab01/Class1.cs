using System;
using System.Collections.Generic;
using System.Text;

namespace lab01
{
    class Class1
    {
        private int PrivField;
        public int PublField;
        protected int ProtField;
       
        private const int privateConst = 5;
        public const int publicConst = 10;
        protected const int protectedConst = 15;

        private int PrivProperty { get; set; }
        public int PublProperty { get; set; }
        protected int ProtProperty { get; set; }
        public Class1(Class1 param1)
        {
            PrivProperty = param1.PrivProperty;
            PublProperty = param1.PublProperty;
            ProtProperty = param1.ProtProperty;
            PrivField = param1.PrivField;
            PublField = param1.PublField;
            ProtField = param1.ProtField;
           
        }

        public Class1() { }

        public Class1(int privateField, int publicField, int protectedField, int privateProperty, int publicProperty, int protectedProperty)
        {
            PrivField = privateField;
            PublField = publicField;
            ProtField = protectedField;
            PrivProperty = privateProperty;
            PublProperty = publicProperty;
            ProtProperty = protectedProperty;
        }

        private void PrivateMethod()
        {
            Console.WriteLine("Приватный метод");
        }
        public void PublicMethod()
        {
            Console.WriteLine("Публичный метод");
        }
        protected void ProtectedMethod()
        {
            Console.WriteLine("Защищенный метод");
        }
    }
}
