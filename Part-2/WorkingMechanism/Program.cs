using System;
using System.Threading.Tasks;

namespace WorkingMechanism
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("1 - Caller (Main Method) - Before await");
            await DoTaskAAsync();
            Console.WriteLine("2 - Caller (Main Method) - After await");


            Console.ReadKey(); // Just to wait till the tasks completes if there is no await
        }

        public static async Task DoTaskAAsync()
        {
            Console.WriteLine("3 - Async Method A (TaskA) - Before async method call without await");
            var taskBTask = DoTaskBAsync();
            Console.WriteLine("4 - Async Method A (TaskA) - Independent Task");
            await taskBTask;
            Console.WriteLine("5 - Async Method A (TaskA) - After await");
        }

        public static async Task DoTaskBAsync()
        {
            Console.WriteLine("6 - Callee Async Method B (TaskB) - Before await");
            await Task.Delay(5000);
            Console.WriteLine("7 - Callee Async Method B (TaskB) - After await");
        }
    }
}
