using System.Collections.Generic;

namespace Task3
{
    class myException : Exception
    {
        myException(string str) : base(str) { }
    }
    class myFieldAccessException : FieldAccessException
    {
        myFieldAccessException(string str) : base(str) { }
    }
    class MyClass
    {
        public string Name { get; set; }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
        public async void DelayWriting()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Проснулись");
        }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyClass myClass = new();
            myClass.Name = "Test";
            Console.WriteLine(myClass.GetHashCode());
            myClass.DelayWriting();
            List<MyClass> values = new() { myClass, new MyClass { Name = "Name" }, new MyClass { Name = "asdf" } };
            foreach(var value in values)
            {
                Console.WriteLine(value.ToString());
            }
        }
    }
}