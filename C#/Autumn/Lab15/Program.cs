using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

static class Program
{
    static int simpleDiapason = 100000;
    static void findSimpleNums()
    {
        int[] simpleNums = new int[simpleDiapason / 2 + 5];
        int simpleIndex = 0;
        for(int i = 1; i<simpleDiapason; i++)
        {
            if((i % 2 == 0 && i != 2) | (i % 3 == 0 && i != 3))
            {
                continue;
            }
            for (int j = 1; j<simpleIndex; j++)
            {
                if (i % simpleNums[j] == 0)
                {
                    continue;
                }
            }
        simpleNums[simpleIndex] = i;
        simpleIndex++;
        }
        StringBuilder stringBuilder = new();
        for (int i = 0; i < simpleIndex; i++)
        {
            stringBuilder.Append(simpleNums[i] + " ");
            if (stringBuilder.Length > 100)
            {
                Console.WriteLine(stringBuilder);
                stringBuilder.Clear();
            }
        }
    }
    static async Task PrintAsync()
    {
        await Task.Delay(3000);
        Console.WriteLine("Дожили");
    }
    static async Task Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Task task = Task.Run(findSimpleNums);
        task.Wait();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        Task cancelTask = new Task(findSimpleNums, token);
        cancelTask.Start();
        Thread.Sleep(stopwatch.Elapsed / 2);
        cancelTokenSource.Cancel();
        Task<string>[] tasks =
        {
            Task.Run(() => "Что"),
            Task.Run(() => "изволить"),
            Task.Run(() => "желаете?"),
        };
        Task.WaitAll(tasks);
        foreach(Task<string> t in tasks)
        {
            Console.Write(t.Result + " ");
        }
        Console.WriteLine();
        Task task1 = new Task(() => Console.WriteLine("Я пришёл"));
        Task task2 = task1.ContinueWith(task1 => Console.WriteLine("Я ждал тебя..."));
        TaskAwaiter taskAwaiter = task1.GetAwaiter();
        taskAwaiter.OnCompleted(() => Console.WriteLine("Урра!"));
        task1.Start();
        task2.Wait();
        int[] ints = new int[10];
        Parallel.For(0, 10, i =>
        {
            ints[i] = i;
        });
        Parallel.ForEach(ints, i =>
        {
            Console.WriteLine("Операция №" + i);
        });
        Parallel.Invoke(() => Thread.Sleep(500), () => Console.WriteLine("Я такой параллельный!"), () =>
        {
            int i = 5;
            for(int j = i; j < 1000; j++)
            {
                i = j;
            }
        });
        await PrintAsync();
    }
}