using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1 - Task 1");
            Console.WriteLine("2 - Task 2");
            Console.WriteLine("3 - Task 3");
            Console.WriteLine("4 - Task 4");
            Console.WriteLine("0 - Exit");
            Console.Write("Choose option: ");

            string input = Console.ReadLine();

            Console.Clear();

            switch (input)
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

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}