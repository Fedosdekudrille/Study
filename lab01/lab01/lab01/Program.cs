using System;

namespace lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------Первое задание--------");
            Class1 c1 = new Class1();
            c1.PublField = 1;
            c1.PublProperty = 2;
            c1.PublicMethod();

            Class1 c12 = new Class1(c1);

            Class1 c13 = new Class1(1, 2, 3, 4, 5, 6);

            Console.WriteLine("-------Второе задание--------");

            Class2 c2 = new Class2();

            c2.InterfaceMethod();

            c2.Event += () => Console.WriteLine("Вызов события");
            c2.RaiseEvent();

            c2.Property = 10;
            Console.WriteLine(c2.Property);

            c2[2] = 5;
            Console.WriteLine(c2[2]);
            


            Console.WriteLine("-------Третье задание--------");

            Class4 objC4 = new Class4();
            objC4.Method1();
            objC4.Method2();
            objC4.B = 200;
            objC4.C = 300;
            Console.WriteLine("Значение B " + objC4.B);
            Console.WriteLine("Значение C: " + objC4.C);
            Console.ReadLine();
           
        }
    }
}
