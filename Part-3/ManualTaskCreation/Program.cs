using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace ManualTaskCreation
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Creating Random Number");

            var result = await GetRandomNumberWithinAsync(10);

            Console.WriteLine($"Result is -> {result}");
        }

        public static Task<int> GetRandomNumberWithinAsync(int maxValue)
        {
            var tcs = new TaskCompletionSource<int>();

            try
            {
                GetRandomNumberWithin(maxValue, (result) =>
                {
                    tcs.SetResult(result);
                });
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }

            return tcs.Task;
        }

        public static void GetRandomNumberWithin(int maxValue, Action<int> callback)
        {
            if (maxValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }

            Thread.Sleep(2000); // Just for demo purpose

            var random = new Random();
            callback(random.Next(maxValue));
        }
    }
}
