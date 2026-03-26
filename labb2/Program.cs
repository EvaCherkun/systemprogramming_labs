using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== PARALLEL PROGRAM MENU =====");
            Console.WriteLine("1 - Task 1 (Numbers & Letters)");
            Console.WriteLine("2 - Task 2 (Task IDs)");
            Console.WriteLine("3 - Task 3 (ContinueWith)");
            Console.WriteLine("4 - Task 4 (Parallel.Invoke)");
            Console.WriteLine("5 - Parallel.For");
            Console.WriteLine("6 - Parallel.For Break");
            Console.WriteLine("7 - Parallel.ForEach");
            Console.WriteLine("0 - Exit");
            Console.Write("Select option: ");

            string choice = Console.ReadLine();

            Console.Clear();

            switch (choice)
            {
                case "1":
                    Task1.Run();
                    break;
                case "2":
                    Task2.Run();
                    break;
                case "3":
                    Task3.Run();
                    break;
                case "4":
                    Task4.Run();
                    break;
                case "5":
                    ParallelForDemo.Run();
                    break;
                case "6":
                    ParallelBreakDemo.Run();
                    break;
                case "7":
                    ParallelForEachDemo.Run();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}