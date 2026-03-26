using System;
using System.Threading;
using System.Threading.Tasks;

public class Task2
{
    public static void Run()
    {
        Action action = () =>
        {
            Console.WriteLine($"Task {Task.CurrentId} started");

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Task {Task.CurrentId}: {i}");
                Thread.Sleep(200);
            }

            Console.WriteLine($"Task {Task.CurrentId} finished");
        };

        Task t1 = new Task(action);
        Task t2 = new Task(action);
        Task t3 = new Task(action);

        t1.Start();
        t2.Start();
        t3.Start();

        Console.WriteLine($"t1 Id = {t1.Id}");
        Console.WriteLine($"t2 Id = {t2.Id}");
        Console.WriteLine($"t3 Id = {t3.Id}");

        Task.WaitAll(t1, t2, t3);
    }
}