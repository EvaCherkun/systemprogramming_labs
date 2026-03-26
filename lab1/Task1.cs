using System;
using System.Threading;

class Task1
{
    public static void Run()
    {
        Thread t1 = new Thread(PrintNumbers);
        Thread t2 = new Thread(PrintLetters);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();
    }

    static void PrintNumbers()
    {
        for (int i = 1; i <= 40; i++)
        {
            Console.WriteLine("Numbers: " + i);
            Thread.Sleep(200);
        }
    }

    static void PrintLetters()
    {
        for (char c = 'A'; c <= 'Z'; c++)
        {
            Console.WriteLine("Letters: " + c);
            Thread.Sleep(300);
        }
    }
}