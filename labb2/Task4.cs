using System;
using System.Threading;
using System.Threading.Tasks;

public class Task4
{
    public static void Run()
    {
        Console.Write("Enter number for factorial: ");
        int n = int.Parse(Console.ReadLine());

        void Factorial()
        {
            int fact = 1;
            for (int i = 1; i <= n; i++)
                fact *= i;

            Console.WriteLine($"Factorial = {fact}");
        }

        void Sum()
        {
            int sum = 0;
            for (int i = 1; i <= n; i++)
                sum += i;

            Console.WriteLine($"Sum = {sum}");
        }

        void Message()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Working...");
                Thread.Sleep(300);
            }
        }

        Parallel.Invoke(Factorial, Sum, Message);
    }
}