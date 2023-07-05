using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7_From4
{
    abstract class Sentient
    {
        public abstract byte Iq { get; protected set; }
        public int birthYear;
        protected Sentient()
        {
            Random random = new();
            Iq = (byte)random.Next(80, 120);
        }
        public abstract void Arrangement();
        public void Greeting()
        {
            Console.WriteLine($"Hello, I'm {GetType().Name}");
        }
        public static bool Equals(Sentient sentient1, Sentient sentient2)
        {
            return sentient1.Iq == sentient2.Iq;
        }
        public static bool ReferenceEquals(Sentient? objA, Sentient? objB)
        {
            return objA == objB;
        }
        public override int GetHashCode()
        {
            Random random = new Random();
            return Iq + random.Next(1000, 10000);
        }
        public override string? ToString()
        {
            return GetType().Name + " " + Iq;
        }
    }
    class Human : Sentient
    {
        public override byte Iq { get; protected set; }
        public override void Arrangement()
        {
            Console.WriteLine("Я состою из мяса");
        }
    }
    class Trans : Sentient
    {
        public override byte Iq { get; protected set; }
        public int power;
        public Trans()
        {
            Random random = new();
            power = random.Next(1000, 10000);
        }
        public override void Arrangement()
        {
            Console.WriteLine("Меня собрали на уральском металлургическом заводе");
        }
    }
}
