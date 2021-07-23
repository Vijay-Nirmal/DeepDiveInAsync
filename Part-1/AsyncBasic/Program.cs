using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncBasic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Started of application");
            var sw = Stopwatch.StartNew();

            var taskAResult = DoTaskAAsync();

            var taskBResult = DoTaskBAsync();

            await Task.WhenAll(taskAResult, taskBResult);

            Console.WriteLine($"Finished in {sw.Elapsed.TotalSeconds} sec");
        }

        public static async Task DoTaskAAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("TaskA started");

                for (int i = 0; i < 100_000_000; i++)
                {
                    var _ = Guid.NewGuid();
                }

                Console.WriteLine("TaskA Ended");
            });
        }

        public static async Task DoTaskBAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("TaskA started");

                for (int i = 0; i < 100_000_000; i++)
                {
                    var _ = Guid.NewGuid();
                }

                Console.WriteLine("TaskA Ended");
            });
        }
    }
}
