using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ProgressReporting
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var progress = new Progress<DoTaskProgressInfo>();  // OR new Progress<DoTaskProgressInfo>(() => <Code to run when progress changed> )
            progress.ProgressChanged += Progress_ProgressChanged;

            await DoTaskAsync(progress);
        }

        private static void Progress_ProgressChanged(object sender, DoTaskProgressInfo value)
        {
            Console.WriteLine($"Completed - {value.Completed.ToString("N0", CultureInfo.GetCultureInfo("en"))} ---- Completed Percentage - {value.CompletedPercentage}%");
        }

        private static async Task DoTaskAsync(IProgress<DoTaskProgressInfo> progress = default)  // IProgress parameter MUST be optional
        {
            await Task.Run(() =>
            {
                for (int i = 0; i <= 100_000_000; i++)
                {
                    if (i % 10_000_000.0 == 0)
                    {
                        progress?.Report(new DoTaskProgressInfo { Completed = i, CompletedPercentage = i / 1_000_000d });
                    }

                    var _ = Guid.NewGuid();
                }
            });
        }
    }

    public class DoTaskProgressInfo
    {
        public int Completed { get; set; }
        public double CompletedPercentage { get; set; }
    }
}
