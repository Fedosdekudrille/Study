namespace OOP
{
    class Machine
    {
        public const string type = "Viechle";
        private const int serviceYears = 5;
        protected const int serviceMonths = 60;
        public int Wheels { get; protected set; }
        private int Fuel { get; set; }
        protected int Amount { get; set; }

        public string model;
        private int speed;
        protected int passangers;
        public Machine() { }
        public Machine(int wheels, int fuel, string model, int speed, int passangers)
        {
            Wheels = wheels;
            Fuel = fuel;
            this.model = model;
            this.speed = speed;
            this.passangers = passangers;
            Amount = 1;
        }
        public Machine(Machine machine)
        {
            Wheels= machine.Wheels;
            Fuel= machine.Fuel;
            model = machine.model;
            speed = machine.speed;
            passangers = machine.passangers;
            Amount = machine.Amount;
        }
        public void StartEngine() 
        {
            Console.WriteLine("Врумм, скорость " + speed);
            ToService();
        }
        private void StopEngine()
        {
            Console.WriteLine("Остановочка");
        }
        protected void ToService()
        {
            Console.WriteLine("C вас 500р, осталось " + serviceYears);
        }
    }
    interface Moovement
    {
        public void Go();
        public string MyProperty { get; set; }
        public event Action OnStatrt;
        public int this[int index] {get; set; }
    }
    class Sportcar : Moovement
    {
        public const string type = "Viechle";
        private const int serviceYears = 5;
        protected const int serviceMonths = 60;
        public int Wheels { get; protected set; }
        private int Fuel { get; set; }
        protected int Amount { get; set; }

        public string model;
        private int speed;
        protected int[] passangers;
        public Sportcar() { }
        public Sportcar(int wheels, int fuel, string model, int speed, int[] passangers)
        {
            Wheels = wheels;
            Fuel = fuel;
            this.model = model;
            this.speed = speed;
            this.passangers = passangers;
            Amount = 1;
        }
        public Sportcar(Sportcar machine)
        {
            Wheels = machine.Wheels;
            Fuel = machine.Fuel;
            model = machine.model;
            speed = machine.speed;
            passangers = machine.passangers;
            Amount = machine.Amount;
        }
        public void StartEngine()
        {
            Console.WriteLine("Врумм, скорость " + speed);
            ToService();
        }
        private void StopEngine()
        {
            Console.WriteLine("Остановочка");
        }
        protected void ToService()
        {
            Console.WriteLine("C вас 500р, осталось " + serviceYears);
        }
        public string MyProperty { get; set; }
        public event Action OnStatrt;
        public int this[int index] { get { return passangers[index]; } set { passangers[index] = value; } }
        public void Go()
        {
            OnStatrt?.Invoke();
        }
    }
    interface Jumping
    {
        void Up();
    }
    class Human : Jumping
    {
        public string Type { get; set; }
        public virtual void Greeting() => Console.WriteLine("Привет");

        protected string Organ { get; set; }
        protected void ProtGreeting() => Console.WriteLine("Хей");

        private string Thoaghts { get; set; }
        private void priGreeting() => Console.WriteLine("Ура");

        public Human() { Type = "Человек"; }
        public void Up() { Console.WriteLine("К небу!"); }
    }

    class Boy : Human
    {
        public string Pol = "Мальчик";
        public Boy() : base() { Organ = "Капец вообще"; }

        public Boy(string pS, string ppS)
        {
            Type = pS;
            Organ = ppS;
        }

        public void mYvoid() { Console.WriteLine("Без типа"); }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Machine machine1 = new(4, 50, "mers", 40, 4);
            Machine machine2 = new(machine1);
            Machine machine3 = new();
            Console.WriteLine(machine1.model);
            machine1.StartEngine();

            int[] ints = { 4, 6 };
            Sportcar sportcar = new(4, 50, "mers", 40, ints);
            Console.WriteLine(sportcar[0]);

            Boy boy = new();
            boy.Greeting();
            boy.Up();
            boy.mYvoid();
            boy.Pol = "Всё ещё мальчик";
            boy.Type = "Даааа";
        }
    }
}