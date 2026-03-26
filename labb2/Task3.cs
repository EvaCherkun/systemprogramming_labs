using System;
using System.Threading.Tasks;

public class Task3
{
    public static void Run()
    {
        Console.Write("Enter N: ");
        int n = int.Parse(Console.ReadLine());

        Task<int> sumTask = new Task<int>(() =>
        {
            int sum = 0;
            for (int i = 1; i <= n; i++)
                sum += i;

            return sum;
        });

        Task resultTask = sumTask.ContinueWith(t =>
        {
            Console.WriteLine($"Sum = {t.Result}");
        });

        sumTask.Start();
        resultTask.Wait();
    }
}