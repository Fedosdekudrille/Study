using Lab13;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    [Serializable]
    public abstract class Sentient
    {
        public abstract byte Iq { get; set; }
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
    [Serializable]
    public class Human : Sentient
    {
        public override byte Iq { get; set; }
        public override void Arrangement()
        {
            Console.WriteLine("Я состою из мяса");
        }
    }
    [Serializable]
    class Trans : Sentient
    {
        public override byte Iq { get; set; }
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
    sealed class Anounsment
    {
        public static void Info(Sentient sentient)
        {
            sentient.Greeting();
            sentient.Arrangement();
            Console.WriteLine($"IQ равняется {sentient.Iq}\n");
        }
    }
}
abstract class ArmyControl : IEnumerable
{
    abstract public Sentient[] SENTIENT { get; set; }
    public IEnumerator GetEnumerator() => SENTIENT.GetEnumerator();
    public void CountTroops()
    {
        Console.WriteLine($"\nКоличество войск в армии: {SENTIENT.Length}");
    }
    public void FindByBirthYear(int year)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nСущества, появившиеся на свет в {year} году:");
        Console.ForegroundColor = ConsoleColor.White;
        foreach (Sentient sentient in SENTIENT)
        {
            if (sentient.birthYear == year)
            {
                Printer.IAmPrinting(sentient);
            }
        }
    }
    public void FindTransByPower(int minPower)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nТрансформеры мощнее {minPower}:");
        Console.ForegroundColor = ConsoleColor.White;
        foreach (Sentient sentient in SENTIENT)
        {
            if (sentient is Trans trans)
            {
                if (trans.power > minPower)
                    Console.WriteLine($"Трансформер: {trans.power}");
            }
        }
    }
}
class Army : ArmyControl
{
    private Sentient[] Sentient;
    public int Length => Sentient.Length;
    public Army()
    {
        Sentient = new Sentient[10];
        for (int i = 0; i < Sentient.Length; i++)
        {
            Sentient[i] = Randomize();
        }
    }
    public Army(Sentient[] sentient)    {
        Sentient = sentient;
    }
    public Sentient this[int index]
    {
        get { return Sentient[index]; }
        set { Sentient[index] = value; }
    }
    static public Army CreateEmpty(int size)
    {
        return new Army { SENTIENT = new Sentient[size] };
    }
    public Army(int length)
    {
        Sentient = new Sentient[length];
        for (int i = 0; i < Sentient.Length; i++)
        {
            Sentient[i] = Randomize();
        }
    }
    private Sentient Randomize()
    {
        Sentient sentient;
        Random random = new();
        if (Convert.ToBoolean(random.Next(0, 2)))
        {
            sentient = new Human();
        }
        else
        {
            sentient = new Trans();
        }
        sentient.birthYear = random.Next(2000, 2005);
        return sentient;
    }
    public override Sentient[] SENTIENT
    {
        get { return Sentient; }
        set { Sentient = value; }
    }
    public void Write()
    {
        Console.WriteLine();
        foreach (Sentient sentient in SENTIENT)
        {
            Printer.IAmPrinting(sentient);
        }
    }
    public void Add()
    {
        Sentient[] NewSentients = new Sentient[SENTIENT.Length + 1];
        for (int i = 0; i < SENTIENT.Length; i++)
        {
            NewSentients[i] = SENTIENT[i];
        }
        NewSentients[SENTIENT.Length] = Randomize();
        Sentient = NewSentients;
    }
    public void Remove(int number)
    {
        Sentient[] NewSentients = new Sentient[SENTIENT.Length - 1];
        int k = 0;
        for (int i = 0; i < SENTIENT.Length - 1; i++)
        {
            if (i == number)
            {
                continue;
            }
            NewSentients[k] = SENTIENT[i];
            k++;
        }
        Sentient = NewSentients;
    }
}
class Printer
{
    static public void IAmPrinting(Sentient sentient)
    {
        Console.WriteLine(sentient.GetType().Name + " " + sentient.Iq);
    }
}
