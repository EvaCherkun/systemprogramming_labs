using System;
using System.Diagnostics;
using System.Threading.Tasks;

public class ParallelForDemo
{
    public static void Run()
    {
        int size = 1000000;
        double[] array = new double[size];

       
        for (int i = 0; i < size; i++)
            array[i] = i + 1;

   
        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < size; i++)
        {
            array[i] = Math.Exp(array[i]) / Math.Pow(array[i], Math.PI);
        }

        sw.Stop();
        Console.WriteLine($"Sequential: {sw.ElapsedMilliseconds} ms");

     
        sw.Restart();

        Parallel.For(0, size, i =>
        {
            array[i] = Math.Exp(array[i]) / Math.Pow(array[i], Math.PI);
        });

        sw.Stop();
        Console.WriteLine($"Parallel: {sw.ElapsedMilliseconds} ms");
    }
}