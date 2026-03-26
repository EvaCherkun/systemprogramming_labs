using System;
using System.Threading;
using System.Threading.Tasks;

public class Task1
{
    public static void Run()
    {
        Task t1 = new Task(() =>
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"Number: {i}");
                Thread.Sleep(200);
            }
        });

        Task t2 = new Task(() =>
        {
            for (char c = 'A'; c <= 'J'; c++)
            {
                Console.WriteLine($"Letter: {c}");
                Thread.Sleep(200);
            }
        });

        t1.Start();
        t2.Start();

        Task.WaitAll(t1, t2);
    }
}