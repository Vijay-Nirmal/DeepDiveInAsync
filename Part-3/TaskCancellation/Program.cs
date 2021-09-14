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
            cancellationTokenSource.CancelAfter(1000); // Cancel() -> Use this to Cancel that task immediately

            try
            {
                await Task.Delay(3000, cancellationTokenSource.Token);  // Just for demo purpose
                // await httpClient.GetStringAsync  -> Real world use case

                // await DoTaskAAsync(cancellationTokenSource.Token); // Our own CancellationToken implementation

                Console.WriteLine("1 - Task has been completed successfully without cancellation");
            }
            catch (OperationCanceledException e)  // Cancelled task MUST throw OperationCanceledException
            {
                if (e is TaskCanceledException)
                {
                    // Task was cancelled before even starting (Throws from Task.Run method)
                }

                Console.WriteLine("2 - Task is cancelled");
            }

            Console.WriteLine("3 - Application Ended");

            Console.ReadKey();
        }

        public static async Task DoTaskAAsync(CancellationToken cancellationToken = default)
        {
            // Before stating the work, its good to check if CancellationRequested, then throw immediately
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Run(() =>
            {
                Console.WriteLine("4 - TaskA started");

                for (int i = 0; i < 100_000_000; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        // Here we can choose to obey the request or discord the request
                        // because sometime we MUST not cancel an operation that is already started

                        // If there are any unmanaged objects, make sure to clean up (Dispose)
                        Console.WriteLine("5 - TaskA Cancelled");
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    var _ = Guid.NewGuid();
                }

                Console.WriteLine("6 - TaskA Ended");
            }, cancellationToken);  // cancellationToken passed here DOESN'T stop the task if it was already started. If the task is not yet started and CancellationRequest is raised then it will throw the TaskCanceledException
        }
    }
}
