namespace Lab2
{
    partial class Customer
    {
        static bool work = false;
        static int allClients = 0;
        public readonly int id;
        public int money, creditCard;
        public int newCreditCard {
            get
            {
                return creditCard;
            }
            private set
            {
                Random random = new();
                creditCard = random.Next(10000000, 99999999);
            }
        } 
        public string surname, name, patronymic, adress;
        public Customer Build(Customer customer, out int money)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nВведите данные о пользователе\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("surname - ");
            customer.surname = Console.ReadLine();
            Console.Write("name - ");
            customer.name = Console.ReadLine();
            Console.Write("patronymic - ");
            customer.patronymic = Console.ReadLine();
            Console.Write("adress - ");
            customer.adress = Console.ReadLine();
            Console.Write("money - ");
            int.TryParse(Console.ReadLine(), out money);
            Console.WriteLine();
            return customer;
        }
        private Customer()
        {
            Build(this, out this.money);
            Random random = new Random();
            id = random.Next(1000, 9999);
            newCreditCard = 1;
            allClients++;
        }
        public Customer(int id, string surname, string name, string patronymic, string adress, params int[] param)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.adress = adress;
            this.creditCard = param[0];
            this.money = param[1];
            allClients++;
        }
        static public Customer Get()
        {
            Customer customer = new Customer();
            return customer;
        }
        static Customer()
        {
            if (!work)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nДобро пожаловать на работу!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public int GetHashCode()
        {
            return id;
        }
        public bool Equals(Customer customer)
        {
            if(customer.surname == surname&& customer.name == name && customer.patronymic == patronymic)
            {
                return true;
            }
            return false;
        }
        public void ToString()
        {
            Console.WriteLine("id - " + id);
            Console.WriteLine("surname - " + surname);
            Console.WriteLine("name - " + name);
            Console.WriteLine("patronymic - " + patronymic);
            Console.WriteLine("adress - " + adress);
            Console.WriteLine("creditCard - " + creditCard);
            Console.WriteLine("money - " + money);
        }
    }
    partial class Customer
    {
        static public void CustomerInfo()
        {
            Console.WriteLine("\n" + allClients);
        }
    }
    static class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new(1, "1", "1", "1", "Neimanskaya, 12, 59", 03284985, 300);
            
            customer.ToString();

            Customer customer2 = Customer.Get();

            customer2.ToString();

            Customer.CustomerInfo();

            Console.WriteLine(customer.Equals(customer2));

            Console.WriteLine(customer.GetHashCode());

            var visiter = new Customer(4, "Abramovich", "Moysha", "Markovich", "Odessa", 93023451, 12342345);

            Customer[] allClients =
            {
                customer,
                customer2,
                new(3, "Rabinovich", "Izya", "Moyiseevich", "Israel", 91239123, 30023432),
                visiter,
            };
            for (int i = 0; i < allClients.Length; i++)
            {
                for(int j = i + 1; j < allClients.Length; j++)
                {
                    if (string.Compare(allClients[i].surname, allClients[j].surname) == 1)
                    {
                        Customer swap = allClients[i];
                        allClients[i] = allClients[j];
                        allClients[j] = swap;
                    }
                }
            }
            foreach(Customer i in allClients)
            {
                Console.WriteLine();
                i.ToString();
            }
            bool prem = false;
            foreach (Customer i in allClients)
            {   
                if(i.creditCard > 90000000 && i.creditCard < 99999999)
                {
                    if (!prem)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Премиум клиенты:");
                        Console.ForegroundColor = ConsoleColor.White;
                        prem = true;
                    }
                    Console.WriteLine();
                    i.ToString();
                }
            }
            Console.WriteLine("\n" + visiter.GetType().ToString());
        }
    }
}