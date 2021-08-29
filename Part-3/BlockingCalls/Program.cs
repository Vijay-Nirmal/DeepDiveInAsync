using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlockingCalls
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var result = await Task.Run(async () =>
            {
                Thread.Sleep(200); // Just for demo purpose to stable things out before taking the number of threads
                ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);

                Console.WriteLine($"Inside GetSomethingAsync Method -> ThreadId: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Available Threads: {workerThreads}");

                Thread.Sleep(200); // Just for demo purpose

                // return GetSomethingAsync().GetAwaiter().GetResult();  // Blocks the caller thread until the inner task got complete. We can notice a drop in Available Threads in ThreadPool
                return await GetSomethingAsync();  // Doesn't block. Even though we are creating new thread inside the current thread will be free, so there won't be a drop in  Available Threads in ThreadPool
            });
        }

        public static async Task<int> GetSomethingAsync()
        {
            var result = await Task.Run(() =>
            {
                Thread.Sleep(200); // Just for demo purpose
                ThreadPool.GetAvailableThreads(out int workerThreads, out int _);

                Console.WriteLine($"\n\nInside GetSomethingAsync Method -> ThreadId: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Available Threads: {workerThreads}");

                Thread.Sleep(200); // Just for demo purpose

                return 5;
            });

            return result;
        }
    }
}
