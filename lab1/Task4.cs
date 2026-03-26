using System;
using System.Collections.Generic;
using System.Threading;

class Task4
{
    class MyThread
    {
        public Thread Thread;
        public long Count = 0;
        public string Name;
        public int Limit;

        public MyThread(string name, ThreadPriority priority, int limit)
        {
            Name = name;
            Limit = limit;

            Thread = new Thread(Run);
            Thread.Name = name;
            Thread.Priority = priority;
        }

        public void Start() => Thread.Start();
        public void Join() => Thread.Join();

        private void Run()
        {
            while (Count < Limit)
            {
                Count++;

                if (Count % (Limit / 10) == 0) 
                {
                    Console.WriteLine($"{Name}: {Count}/{Limit}");
                }
            }

            Console.WriteLine($"{Name} finished.");
        }
    }

    public static void Run()
    {
        Console.Write("Введіть кількість потоків: ");
        int n = int.Parse(Console.ReadLine());

        List<MyThread> threads = new List<MyThread>();

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nПотік {i + 1}:");

            Console.Write("Ім’я: ");
            string name = Console.ReadLine();

            Console.WriteLine("Пріоритет (0-Lowest, 1-BelowNormal, 2-Normal, 3-AboveNormal, 4-Highest): ");
            int p = int.Parse(Console.ReadLine());

            ThreadPriority priority = (ThreadPriority)p;

            Console.Write("До якого числа рахувати: ");
            int limit = int.Parse(Console.ReadLine());

            threads.Add(new MyThread(name, priority, limit));
        }

     
        foreach (var t in threads)
            t.Start();

        foreach (var t in threads)
            t.Join();

        long total = 0;
        foreach (var t in threads)
            total += t.Count;

        Console.WriteLine("\n=== Результати ===");

        foreach (var t in threads)
        {
            double percent = (double)t.Count / total * 100;
            Console.WriteLine($"{t.Name}: {t.Count} ({percent:F2}%)");
        }
    }
}