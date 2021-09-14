using System;
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

        // Converting non asynchronous method to asynchronous method
        public static Task<int> GetRandomNumberWithinAsync(int maxValue)
        {
            var tcs = new TaskCompletionSource<int>();

            try
            {
                GetRandomNumberWithin(maxValue, (result) =>
                {
                    Console.WriteLine($"Setting task result");
                    tcs.SetResult(result);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Setting exception");
                tcs.SetException(e);
            }

            Console.WriteLine($"Returning Task");
            return tcs.Task;
        }

        // Assume this is a build-in non asynchronous method 
        public static void GetRandomNumberWithin(int maxValue, Action<int> callback)
        {
            if (maxValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }

            // Just for demo purpose to simulate a delay before sending the result
            Task.Delay(8000).ContinueWith(_ =>
            {
                var random = new Random();
                callback(random.Next(maxValue));
            });
        }
    }
}
