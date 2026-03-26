using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ParallelForEachDemo
{
    public static void Run()
    {
        List<double> numbers = new List<double>();

        for (int i = 1; i <= 20; i++)
            numbers.Add(i);

        Parallel.ForEach(numbers, num =>
        {
            double result = Math.Exp(num) / Math.Pow(num, Math.PI);
            Console.WriteLine($"Value: {num} -> Result: {result:F2}");
        });
    }
}