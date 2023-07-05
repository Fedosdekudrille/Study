using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab4
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
    sealed class Anousment
    {
        public static void Info(Sentient sentient)
        {
            sentient.Greeting();
            sentient.Arrangement();
            Console.WriteLine($"IQ равняется {sentient.Iq}\n");
        }
    }
    abstract class ArmyControl
    {
        abstract public Sentient[] SENTIENT { get; set; }
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
        public Army()
        {
            Sentient = new Sentient[10];
            for (int i = 0; i < Sentient.Length; i++)
            {
                Sentient[i] = Randomize();
            }
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
    interface IControl
    {
        abstract void Drive();
        abstract void Stop();
        abstract void Break();
    }
    abstract class Viechle
    {
        public abstract void Break();
        public abstract string Type { get; }
        public int price;
        public override string? ToString()
        {
            return GetType().Name + " " + price;
        }
    }
    public struct Engine
    {
        public Engine()
        {
            started = false;
        }
        public bool started;
        public void Start()
        {
            started = true;
        }
    }
    enum CarTypes
    {
        BMW,
        MERSEDES,
        PORSCHE,
        RENO
    }
    partial class Car : Viechle, IControl
    {
        public CarTypes CarTypes { get; set; }
        public override string Type { get { return "Машина"; } }
        public override void Break()
        {
            Console.WriteLine("Пора в автомастерскую");
        }
        void IControl.Break()
        {
            Console.WriteLine("Сломались...");
        }
        public Engine engine = new();
    }
    class Printer
    {
        static public void IAmPrinting(Sentient sentient)
        {
            Console.WriteLine(sentient.GetType().Name + " " + sentient.Iq);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Sentient human = new Human();
            Sentient trans = new Trans();
            Anousment.Info(human);
            Anousment.Info(trans);

            Car car = new()
            {
                price = 5000,
                CarTypes = CarTypes.MERSEDES,
            };
            car.engine.Start();
            Console.WriteLine(car.CarTypes);
            if (car.engine.started)
            {
                car.Drive();
                car.Stop();
                if (car is IControl control)
                {
                    control.Break();
                }
                if (car is Car myCar)
                {
                    car.Break();
                }
            }
            Console.WriteLine(human.Equals(trans));
            Console.WriteLine(human.ToString());
            Console.WriteLine(human.GetHashCode());
            Console.WriteLine(Sentient.Equals(human, trans));
            Console.WriteLine(human.GetType());
            Console.WriteLine(car.ToString());
            //sentient[] viechle =
            //{
            //    new human(),
            //    new trans(),
            //};
            //foreach (sentient sentient in viechle)
            //{
            //    printer.iamprinting(sentient);
            //}
            Army army = new();
            army.Write();
            army.Add();
            army.Write();
            army.Remove(army.SENTIENT.Length - 1);
            army.Write();
            army.FindTransByPower(5000);
            army.CountTroops();
            army.FindByBirthYear(2004);
        }
    }
}