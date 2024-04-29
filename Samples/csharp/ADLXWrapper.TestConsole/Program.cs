using ADLXWrapper.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ADLXWrapper.TestConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await SomeLoop();
        }

        private static async Task SomeLoop()
        {
            using (var adlxHelper = new ADLXWrapper())
            {
                using (var disposable = new CompositeDisposable())
                {
                    var systemServices = adlxHelper.GetSystemServices().DisposeWith(disposable);

                    var gpus = systemServices.GetGPUs();
                    var gpu = gpus.First().DisposeWith(disposable);

                    var tuningServices = systemServices.GetGPUTuningService().DisposeWith(disposable);
                    var manual = tuningServices.GetManualFanTuning(gpu).DisposeWith(disposable);
                    var monitor = systemServices.GetPerformanceMonitor().DisposeWith(disposable);
                    var metrics = monitor.GetGPUMetricsStruct(gpu);
                    

                    foreach (var speed in Enumerable.Range(0, 5).Select(x => x * 20).Reverse())
                    {
                        manual.SetFanTuningStates2(speed);
                        using (var metric = monitor.GetGPUMetrics(gpu))
                        {
                            var gputemp = metric.GetGPUTemperature();
                            var hotspot = metric.GetHotspotTemperature();
                            var rpm = metric.GetFanSpeed();
                            Console.WriteLine($"gpu: {gputemp}, hotspot: {hotspot}, rpm = {rpm}, speed = {speed} %");
                        }

                        await Task.Delay(1000);
                    }

                    
                    manual.SetFanTuningStates2(40);
                    manual.Reset();
                    
                }
            }
        }

        private static bool HasError(ADLX_RESULT result, string message)
        {
            if (result == ADLX_RESULT.ADLX_OK)
                return false;

            Console.WriteLine($"ADLX result ({result}): {message}");

            return true;
        }
    }
}
