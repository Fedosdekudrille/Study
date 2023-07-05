namespace Lab8
{
    abstract class Worker
    {
        public byte productivity;
        public int standartSalary;
        public int salary;
    }
    class Student : Worker
    {
        public Student()
        {
            Random random = new();
            productivity = (byte)random.Next(0, 101);
            standartSalary = salary = random.Next(200, 500);
        }
    }
    class Turner : Worker { 
        public Turner()
        {
            Random random = new();
            productivity = (byte)random.Next(0, 101);
            standartSalary = salary = random.Next(1000, 1500);
        }
    }
    static class Director
    {
        public delegate void ChangeSalary(Worker worker, int sum);
        public static event ChangeSalary? IncreaseSalary;
        public static event ChangeSalary? DecreaseSalary;
        public static void Examination(Worker[] workers)
        {
            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].productivity > 80)
                {
                    IncreaseSalary?.Invoke(workers[i], 50);
                }
                else if (workers[i].productivity < 20)
                {
                    DecreaseSalary?.Invoke(workers[i], 30);
                }
            }
        }
        public static void ChooseFavorietes(Worker[] workers)
        {
            Random random = new();
            foreach (Worker worker in workers)
            {
                if(random.Next(0, 11) == 10)
                {
                    IncreaseSalary?.Invoke(worker, 20);
                }
            }
        }
        public static void ShowSalaries(Worker[] workers)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Зарплаты сотрудников:");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Worker worker in workers)
            {
                if (worker.standartSalary > worker.salary)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if(worker.standartSalary < worker.salary)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(worker.salary + "  ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
        }
    }
    static class StrChange
    {
        public static string DeleteComas(string str)
        {
            string resultString = "";
            foreach(char ch in str)
            {
                if(ch != ',')
                {
                    resultString += ch;
                }
            }
            return resultString;
        }
        public static string DeleteOddSpaces(string str)
        {
            string resultString = "";
            bool spaceFound = false;
            foreach (char ch in str)
            {
                if(ch == ' ')
                {
                    if (spaceFound)
                    {
                        continue;
                    }
                    spaceFound = true;
                }
                else
                {
                    spaceFound = false;
                }
                resultString += ch;
            }
            return resultString;
        }
        public static string FirstToCapital(string str)
        {
            if (str[0] >= 'а' && str[0] <= 'я')
            {
                string resultString = "";
                resultString += (char)(str[0] - 32);
                for (int i = 1; i < str.Length; i++)
                {
                    resultString += str[i];
                }
                return resultString;
            }
            return str;
        }
        public static void WriteWordsReverse(string str)
        {
            string[] words = str.Split();
            for(int i = 0; i < words.Length; i++)
            {
                for(int j = words[i].Length - 1; j >= 0; j--)
                {
                    Console.Write(words[i][j]);
                }
                Console.Write(' ');
            }
            Console.WriteLine();
        }
        public static bool IsIncentive(string str)
        {
            if (str[str.Length - 1] == '!')
            {
                return true;
            }
            return false;
        }
    }
    internal class Program
    {
        static void Task1()
        {
            Worker[] workers = new Worker[20];
            for (int i = 0; i < workers.Length; i++)
            {
                Random rand = new();
                if (Convert.ToBoolean(rand.Next(0, 3)))
                {
                    workers[i] = new Turner();
                }
                else
                {
                    workers[i] = new Student();
                }
            }
            Director.IncreaseSalary += (Worker worker, int sum) => worker.salary += sum;
            Director.DecreaseSalary += (Worker worker, int sum) => worker.salary -= sum;
            Director.ShowSalaries(workers);
            Director.Examination(workers);
            Director.ChooseFavorietes(workers);
            Director.ShowSalaries(workers);
        }
        static void Task2()
        {
            Console.WriteLine("\nЗадание 2:");
            string str = "съешь  ещё этих мягких, французских  булочек.";
            Action<string> action = StrChange.WriteWordsReverse;
            Predicate<string> predicate = StrChange.IsIncentive;
            Func<string, string>? func = StrChange.DeleteComas;
            Console.WriteLine(str);
            str = func(str);
            func = StrChange.DeleteOddSpaces;
            str = func(str);
            func = StrChange.FirstToCapital;
            str = func(str);
            Console.WriteLine(str);
            action(str);
            if (predicate(str))
            {
                Console.WriteLine("Это предложение восклицательное!");
            }
            else
            {
                Console.WriteLine("Это предложение не восклицательное.");
            }
        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
        }
    }
}