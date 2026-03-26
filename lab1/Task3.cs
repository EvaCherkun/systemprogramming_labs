using System;
using System.Threading;

class Task3
{
    public static void Run()
    {
        MyThread t1 = new MyThread("Normal", ThreadPriority.Normal);
        MyThread t2 = new MyThread("AboveNormal", ThreadPriority.AboveNormal);
        MyThread t3 = new MyThread("Highest", ThreadPriority.Highest);

        t1.Start();
        t2.Start();
        t3.Start();

        t1.Join();
        t2.Join();
        t3.Join();

        long total = t1.Count + t2.Count + t3.Count;

        Console.WriteLine($"{t1.Name}: {(double)t1.Count / total * 100:F2}%");
        Console.WriteLine($"{t2.Name}: {(double)t2.Count / total * 100:F2}%");
        Console.WriteLine($"{t3.Name}: {(double)t3.Count / total * 100:F2}%");
    }

    class MyThread
    {
        public Thread thread;
        public long Count;
        public string Name;

        public MyThread(string name, ThreadPriority priority)
        {
            Name = name;
            thread = new Thread(Run);
            thread.Priority = priority;
        }

        public void Start() => thread.Start();
        public void Join() => thread.Join();

        void Run()
        {
            while (Count < 100_000_000)
            {
                Count++;
            }
        }
    }
}