using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;

namespace Lab14
{
    internal class Program
    {
        static int x = 0;
        static int maxN;
        static object locker = new();
        static int oddNumber = 1;
        static int OddNumber
        {
            get { return oddNumber; }
            set { oddNumber = value + 1; }
        }
        static int evenNumber = 2;
        static int EvenNumber
        {
            get { return evenNumber; }
            set { evenNumber = value + 1; }
        }
        struct Task4
        {
            public bool isOdd;
            public Task4(bool odd)
            {
                isOdd = odd;
            }
            public void Method()
            {
                lock (locker)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " " + (isOdd ? OddNumber++ : EvenNumber++));
                }
            }
        }
        static void Task3()
        {
            Thread thread = Thread.CurrentThread;
            thread.Name = "Task3";
            for (int i = 0; i < maxN; i++)
            {
                Console.WriteLine($"{i} - {thread.Name} {thread.ManagedThreadId} {thread.CurrentCulture} {thread.Priority} {thread.ThreadState}");
                Thread.Sleep(50);
            }
        }
        static void Main(string[] args)
        {
            AssemblyLoadContext context = new(name: "WriteInColor", isCollectible: true);
            string assemblyPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, @"Color\bin\Debug\net6.0\Color.dll");
            Assembly colorAssembly = context.LoadFromAssemblyPath(assemblyPath);
            Type WriteInColor = colorAssembly.GetType("Color.WriteInColor");
            MethodInfo writeRed = WriteInColor.GetMethod("Red");
            int alarm = 0;
            Timer timer = new(new TimerCallback(o => writeRed.Invoke(null, new object[1] { o })), $"Сообщение отправляется раз в 100 миллисекунд", 100, 100);
            Process currentProcess = Process.GetCurrentProcess();
            foreach(Process process in Process.GetProcesses())
            {
                string str = $"Id: {process.Id}; Имя: {process.ProcessName}; Виртуальная память: {process.VirtualMemorySize64}\n";
                if (process.Id == currentProcess.Id)
                {
                    WriteInColor.GetMethod("Green")?.Invoke(null, new object[1] { str });
                }
                else
                {
                    Console.WriteLine(str);
                }
            }

            AppDomain appDomain = AppDomain.CurrentDomain;
            Console.WriteLine($"Имя: {appDomain.FriendlyName}");
            Console.WriteLine($"Папка: {appDomain.BaseDirectory}");
            Assembly[] assemblies = appDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Console.WriteLine(assembly.GetName().Name);
            }
            Assembly asm = Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), "Lab14.dll"));
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
            {
                Console.WriteLine(t.Name);
            }
            for(int i = 0; i < 10; i++)
            {
                Task4 odd = new(true);
                Task4 even = new(false);
                Thread oddThread = new(odd.Method);
                oddThread.Name = "odd: ";
                Thread evenThread = new(even.Method);
                evenThread.Name = "even: ";
                oddThread.Start();
                evenThread.Start();
            }
            Thread task3 = new(Task3);
            maxN = 5;
            task3.Start();

            context.Unload();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}