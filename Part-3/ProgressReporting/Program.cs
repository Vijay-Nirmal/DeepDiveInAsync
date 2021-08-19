using System;
using System.Threading.Tasks;

namespace ProgressReporting
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var progress = new Progress<int>();  // OR new Progress<int>(() => Code to run when progress changed; )
            progress.ProgressChanged += Progress_ProgressChanged;

            await DoTaskAsync(progress);
        }

        private static void Progress_ProgressChanged(object sender, int value)
        {
            Console.WriteLine($"Current progress - {value}");
        }

        private static async Task DoTaskAsync(IProgress<int> progress = default)  // IProgress parameter MUST be optional
        {
            await Task.Run(() =>
            {
                for (int i = 0; i <= 100_000_000; i++)
                {
                    if (i % 10_000_000.0 == 0)
                    {
                        progress?.Report(i / 10_000_000);
                    }

                    var _ = Guid.NewGuid();
                }
            });
        }
    }
}
