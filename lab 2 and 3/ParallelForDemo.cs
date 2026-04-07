using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] sizes = { 1000000, 5000000, 10000000 };

     
        foreach (int n in sizes)
        {
            Console.WriteLine($"\nРозмір масиву: {n}");

          
            for (int formula = 1; formula <= 4; formula++)
            {
                Console.WriteLine($"\nФормула {formula}");

                double[] array1 = new double[n];
                double[] array2 = new double[n];

                
                for (int i = 0; i < n; i++)
                {
                    array1[i] = i + 1;
                    array2[i] = i + 1;
                }

                // ================= ПОСЛІДОВНО =================
                Stopwatch sw1 = Stopwatch.StartNew();

                for (int i = 0; i < n; i++)
                {
                    double x = array1[i];

                    switch (formula)
                    {
                        case 1:
                            array1[i] = x / 10;
                            break;

                        case 2:
                            array1[i] = x / Math.PI;
                            break;

                        case 3:
                            array1[i] = Math.Exp(x) / Math.Pow(x, Math.PI);
                            break;

                        case 4:
                            array1[i] = Math.Exp(Math.PI * x) / Math.Pow(x, Math.PI);
                            break;
                    }
                }

                sw1.Stop();

                // ================= ПАРАЛЕЛЬНО =================
                Stopwatch sw2 = Stopwatch.StartNew();

                Parallel.For(0, n, i =>
                {
                    double x = array2[i];

                    switch (formula)
                    {
                        case 1:
                            array2[i] = x / 10;
                            break;

                        case 2:
                            array2[i] = x / Math.PI;
                            break;

                        case 3:
                            array2[i] = Math.Exp(x) / Math.Pow(x, Math.PI);
                            break;

                        case 4:
                            array2[i] = Math.Exp(Math.PI * x) / Math.Pow(x, Math.PI);
                            break;
                    }
                });

                sw2.Stop();

                Console.WriteLine($"Послідовно: {sw1.ElapsedMilliseconds} мс");
                Console.WriteLine($"Паралельно: {sw2.ElapsedMilliseconds} мс");
            }
        }

        Console.WriteLine("\nГотово!");
        Console.ReadLine();
    }
}

