using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
using System.Text.Json;

namespace Task4
{
    class Props
    {
        public int NumProp { get; private set; }
        public Props(int numProp)
        {
            NumProp = numProp;
        }

        public int NumberFive { get { return 5; } }
        public static bool operator ==(Props prop, int num)
        {
            return prop.NumProp == num;
        }
        public static bool operator !=(Props prop, int num)
        {
            return prop.NumProp != num;
        }
    }
    class Fields
    {
        public int f;
        public static int sF;
        static Fields()
        {
            Console.WriteLine("Fields применён впервые!");
        }
    }
    class Event
    {
        public static event Func<int>? myEvent;
        public static int DoIt()
        {
            return myEvent();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrayList collection = new();
            Fields.sF = 1;
            collection.Add(new Fields() { f = 5 });
            collection.Add(new Props(1));
            collection.Add(new Fields() { f = 1 });
            foreach(object el in collection)
            {
                if(el is Fields fields)
                { 
                    Event.myEvent += el.GetHashCode;
                    Console.WriteLine(Event.DoIt());
                }
            }
            var arr = collection.ToArray().Where((el)=>{ return el is Fields; }).Select((el) =>
            {
                Fields fields = el as Fields;
                return new
                {
                    fields.f,
                    Fields.sF
                };
            });
            foreach(var el in arr)
            {
                Console.WriteLine(el.f + " " + el.sF);
            }
            using (FileStream fileStream = new(@"..\..\..\json.json", FileMode.OpenOrCreate))
                foreach (object el in collection)
                    JsonSerializer.Serialize(fileStream, el);
        }
    }
}