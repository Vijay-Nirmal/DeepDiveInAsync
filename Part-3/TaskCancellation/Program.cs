using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancellation
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(6000); // Cancel() -> Use this to Cancel that task now

            try
            {
                await Task.Delay(5000, cancellationTokenSource.Token);
                // await httpClient.GetStringAsync  -> Real world use case
            }
            catch (OperationCanceledException)  // Cancelled task MUST throw OperationCanceledException
            {
                Console.WriteLine("Task is cancelled");
            }

            Console.WriteLine("Application Ended");
        }
    }
}
