using System;
using System.Threading;

class Task2
{
    static void ForegroundWork()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} working...");
            Thread.Sleep(500);
        }
    }

    static void BackgroundWork()
    {
        while (true)
        {
            Console.WriteLine("Background thread is running...");
            Thread.Sleep(1000);
        }
    }

    static void Main()
    {
        Thread t1 = new Thread(ForegroundWork);
        Thread t2 = new Thread(ForegroundWork);
        Thread bg = new Thread(BackgroundWork);

        t1.Name = "Foreground 1";
        t2.Name = "Foreground 2";

        bg.IsBackground = true;

        t1.Start();
        t2.Start();
        bg.Start();

        Console.ReadLine();
    }

    internal static void Run()
    {
        throw new NotImplementedException();
    }
}