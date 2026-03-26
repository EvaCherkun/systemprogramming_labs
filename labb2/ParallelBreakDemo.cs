using System;
using System.Threading.Tasks;

public class ParallelBreakDemo
{
    public static void Run()
    {
        int[] array = new int[100];

        for (int i = 0; i < array.Length; i++)
            array[i] = i;

        int target = 50;
        int delta = 2;

        Parallel.For(0, array.Length, (i, state) =>
        {
            if (Math.Abs(array[i] - target) <= delta)
            {
                Console.WriteLine($"Found near {target}: {array[i]} at index {i}");
                state.Break();
            }
        });
    }
}