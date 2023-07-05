using Lab2;

namespace Lab10
{
    class WriteInColor
    {
        public static void Green(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Red(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Yellow(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    internal class Program
    {
        static void Task1()
        {
            string[] months =
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "Jule",
                "August",
                "September",
                "October",
                "November",
                "December"
            };
            const int n = 5;
            var monthsByStringLength = from month in months
                                       where month.Length == n
                                       select month;
            WriteInColor.Green($"Месяцы с длиной строки {n}:");
            foreach (string month in monthsByStringLength)
            {
                Console.WriteLine(month);
            }
            var monthsSummerWinter = from month in months
                                     where Array.IndexOf<string>(months, month) <= 1
                                     || Array.IndexOf<string>(months, month) >= 5
                                     && Array.IndexOf<string>(months, month) <= 7
                                     || Array.IndexOf<string>(months, month) == 11
                                     select month;
            WriteInColor.Green($"Зимние и летние месяцы:");
            foreach (string month in monthsSummerWinter)
            {
                Console.WriteLine(month);
            }
            var monthsSortByAlphabet = from month in months
                                       orderby month
                                       select month;
            WriteInColor.Green($"Месяцы, отсортированные в алфавитном порядке:");
            foreach (string month in monthsSortByAlphabet)
            {
                Console.WriteLine(month);
            }
            var monthsULongerFour = months.Where(m => m.Contains('u') && m.Length > 4);
            WriteInColor.Green($"Месяцы, содержащие букву \"u\", длиной более четырёх букв:");
            foreach (var month in monthsULongerFour)
            {
                Console.WriteLine(month);
            }
        }
        static void Task25()
        {
            List<Customer> customers = new()
            {
                new(1, "1", "c", "1", "Neimanskaya, 12, 59", 03284980, 310),
                new(2, "1", "asdf", "1", "Neimanskaya, 12, 59", 03284981, 300),
                new(3, "1", "а", "1", "Neimanskaya, 12, 59", 03284982, 300),
                new(4, "1", "i", "1", "Neimanskaya, 12, 59", 03284983, 300),
                new(5, "1", "sdaf", "1", "Neimanskaya, 12, 59", 03284984, 200),
                new(6, "1", "f", "1", "Neimanskaya, 12, 59", 03284985, 300),
                new(7, "1", "asdf", "1", "Neimanskaya, 12, 59", 03284986, 300),
                new(8, "1", "o", "1", "Neimanskaya, 12, 59", 03284987, 3000),
                new(9, "1", "c", "1", "Neimanskaya, 12, 59", 03284988, 300),
                new(10, "1", "asdf", "1", "Neimanskaya, 12, 59", 03284989, 300),
            };
            var reachest = customers.OrderByDescending(c => c.money).Select(c => new
            {
                money = c.money,
                id = c.id,
            }).First();
            Console.WriteLine($"Богатейшие клиенты: {reachest.id} - {reachest.money}");
            Console.WriteLine(customers.Where(c => c.name == "asdf").First().id);
            foreach (Customer customer in from c in customers orderby c.name select c)
            {
                Console.Write(customer.name + "  ");
            }
            Console.WriteLine("\n");
            var groups = customers.GroupBy(c => c.name);
            foreach (var group in groups)
            {
                Console.Write(group.Key + ": ");
                foreach (Customer customer in group)
                {
                    Console.Write(customer.id + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Сумма всех хранимых денег: " + customers.Aggregate((x, y) => x + y).money);
            Console.WriteLine(customers.All(c => c.money > 100));
            foreach (Customer customer in customers.TakeWhile(c => c.name != "sdaf"))
            {
                Console.Write(customer.id + "  ");
            }
            Console.WriteLine();
            Debt[] debts =
            {
                new(2, 50),
                new(4, 14),
                new(7, 93),
            };
            var customersInDebdt = from customer in customers
                                   join debdt in debts
                                   on customer.id equals debdt.id
                                   select new
                                   {
                                       customer.id,
                                       debdt.debdt,
                                   };
            foreach (var customer in customersInDebdt)
            {
                Console.WriteLine($"Долг клиента {customer.id} составляет {customer.debdt}");
            }
        }
        static void Main(string[] args)
        {
            Task1();
            Task25();
        }
    }
}