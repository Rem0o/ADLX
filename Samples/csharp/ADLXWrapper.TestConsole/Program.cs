using ADLXWrapper.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

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

                    Console.WriteLine($"GPU with name: {gpu.Name}");
                    Console.WriteLine($"Support target fan speed: {manual.SupportsTargetFanSpeed}");
                    Console.WriteLine($"Support zero rpm: {manual.SupportsZeroRPM}");
                    Console.WriteLine($"Speed range: {manual.SpeedRange.Min}-{manual.SpeedRange.Max}");

                    manual.SetZeroRPM(false);

                    if (manual.SpeedRange.Max > 1000 && manual.SupportsTargetFanSpeed)
                    {
                        foreach (var speed in Enumerable.Range(0, 11).Select(x => x * 10))
                        {
                            var speedRpm = (speed / 100f) * manual.SpeedRange.Max;
                            Console.WriteLine($"Setting target fan speed to : {speedRpm} rpm");
                            manual.SetTargetFanSpeed(Convert.ToInt32(speedRpm));
                            await Task.Delay(1000);
                        }
                    }
                    else
                    {
                        foreach (var speed in Enumerable.Range(0, 11).Select(x => x * 10))
                        {
                            Console.WriteLine($"Setting target fan speed to : {speed}");
                            if (manual.SupportsTargetFanSpeed)
                            {
                                manual.SetTargetFanSpeed(speed);
                            }
                            else
                            {
                                manual.SetFanTuningStates2(speed);
                            }
                            await Task.Delay(1000);
                        }
                    }

                    manual.Reset();
                }
            }
        }
    }
}
