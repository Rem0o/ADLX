using ADLXWrapper.Bindings;
using System;
using System.Threading.Tasks;

namespace ADLXWrapper.TestConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var disposable = new CompositeDisposable())
            {
                var adlxHelper = new ADLXHelper().DisposeWith(disposable);
                if (HasError(adlxHelper.Initialize(), "Couldn't initialize ADLX"))
                    return;

                var systemServices = adlxHelper.GetSystemServices().DisposeWith(disposable);

                var gpuListPtr = ADLX.new_gpuListP_Ptr().DisposeWith(ADLX.delete_gpuListP_Ptr, disposable);
                if (HasError(systemServices.GetGPUs(gpuListPtr), "Couldn't get GPU list"))
                    return;

                var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr).DisposeWith(disposable);

                var servicePtr = ADLX.new_gpuTuningP_Ptr().DisposeWith(ADLX.delete_gpuTuningP_Ptr, disposable);
                if (HasError(systemServices.GetGPUTuningServices(servicePtr), "Couldn't get GPU tuning"))
                    return;

                var tuningServices = ADLX.gpuTuningP_Ptr_value(servicePtr).DisposeWith(disposable);

                var index = gpuList.Begin();
                var gpuPtr = ADLX.new_gpuP_Ptr().DisposeWith(ADLX.delete_gpuP_Ptr, disposable);
                if (HasError(gpuList.At(index, gpuPtr), "Couldn't get GPU"))
                    return;

                var gpu = ADLX.gpuP_Ptr_value(gpuPtr).DisposeWith(disposable);

                var interfacePtr = ADLX.new_adlxInterfaceP_Ptr().DisposeWith(ADLX.delete_adlxInterfaceP_Ptr, disposable);

                if (HasError(tuningServices.GetManualFanTuning(gpu, interfacePtr), "Couldn't get interface"))
                    return;

                var @interface = ADLX.adlxInterfaceP_Ptr_value(interfacePtr).DisposeWith(disposable);
                var manual = ADLX.CastManualFanTuning(@interface).DisposeWith(disposable);

                var boolPtr = ADLX.new_boolP().DisposeWith(ADLX.delete_boolP, disposable);
                if (HasError(manual.IsSupportedTargetFanSpeed(boolPtr), "Could not check for is supported"))
                    return;

                var isSupported = ADLX.boolP_value(boolPtr);

                Console.WriteLine($"Set target fan speed is supported: {isSupported}");

                var targetFanSpeedPtr = ADLX.new_intP().DisposeWith(ADLX.delete_intP, disposable);
                if (!HasError(manual.GetTargetFanSpeed(targetFanSpeedPtr), "Could not get target Fan speed"))
                    Console.WriteLine($"Current target fan speed is: {ADLX.intP_value(targetFanSpeedPtr)}");

                if (isSupported)
                {
                    manual.SetTargetFanSpeed(50);
                    Console.WriteLine("Setting fan speed to 50");
                }

                var zeroRpmPtr = ADLX.new_boolP().DisposeWith(ADLX.delete_boolP, disposable);
                manual.GetZeroRPMState(zeroRpmPtr);
                var isZeroRPM = ADLX.boolP_value(zeroRpmPtr);
                Console.WriteLine("Current zero RPM status: " + isZeroRPM);

                manual.IsSupportedZeroRPM(zeroRpmPtr);
                var zeroRPMSupported = ADLX.boolP_value(zeroRpmPtr);
                Console.WriteLine($"Zero RPM supported: {zeroRPMSupported}");

                if (zeroRPMSupported && HasError(manual.SetZeroRPMState(true), "Coudln't set 0 rpm mode"))
                    return;

                if (zeroRPMSupported && HasError(manual.SetZeroRPMState(false), "Coudln't reset 0 rpm mode"))
                    return;

                ///  SETTING FAN SPEED
                for (int v = 0; v < 100; v++)
                {
                    SetFanSpeed(manual, v);
                    await Task.Delay(1500);
                }

                var performancePtr = ADLX.new_performanceP_Ptr().DisposeWith(ADLX.delete_performanceP_Ptr, disposable);
                if (HasError(systemServices.GetPerformanceMonitoringServices(performancePtr), "Couldn't get performance monitor"))
                    return;

                var performance = ADLX.performanceP_Ptr_value(performancePtr).DisposeWith(disposable);
                var metricsPtr = ADLX.new_metricsP_Ptr().DisposeWith(ADLX.delete_metricsP_Ptr, disposable);
                if (HasError(performance.GetCurrentGPUMetrics(gpu, metricsPtr), "Couldn't get metrics"))
                    return;

                IADLXGPUMetrics metrics = ADLX.metricsP_Ptr_value(metricsPtr);

                var doublePtr = ADLX.new_doubleP().DisposeWith(ADLX.delete_doubleP, disposable);
                if (HasError(metrics.GPUTemperature(doublePtr), "Couldn't get temp"))
                    return;

                var temp = ADLX.doubleP_value(doublePtr);
                Console.WriteLine($"Temp is {temp}");

                if (HasError(metrics.GPUHotspotTemperature(doublePtr), "Couldn't get hotspot temp"))
                    return;

                temp = ADLX.doubleP_value(doublePtr);
                Console.WriteLine($"Hotspot Temp is {temp}");

                var intPtr = ADLX.new_intP().DisposeWith(ADLX.delete_intP, disposable);
                if (HasError(metrics.GPUFanSpeed(intPtr), "Couldn't get fan speed"))
                    return;

                var fanSpeed = ADLX.intP_value(intPtr);
                Console.WriteLine($"Fan speed is {fanSpeed}");
            }
        }

        private static void SetFanSpeed(IADLXManualFanTuning manual, int speed)
        {
            using (var disposable = new CompositeDisposable())
            {
                var stateListPtr = ADLX.new_fanTuningStateListP_Ptr().DisposeWith(ADLX.delete_fanTuningStateListP_Ptr, disposable); ;
                if (HasError(manual.GetFanTuningStates(stateListPtr), "Couldn't get the fan tuning states"))
                    return;

                var stateList = ADLX.fanTuningStateListP_Ptr_value(stateListPtr).DisposeWith(disposable);

                for (uint i = stateList.Begin(); i < stateList.End(); i++)
                {
                    var statePtr = ADLX.new_fanTuningStateP_Ptr().DisposeWith(ADLX.delete_fanTuningStateP_Ptr, disposable);
                    HasError(stateList.At(i, statePtr), $"Couldn't get state {i}");
                    var state = ADLX.fanTuningStateP_Ptr_value(statePtr).DisposeWith(disposable);

                    state.SetFanSpeed(speed);
                }

                var errorIndexPtr = ADLX.new_intP().DisposeWith(ADLX.delete_intP, disposable); ;
                HasError(manual.IsValidFanTuningStates(stateList, errorIndexPtr), "Couldn't validate states");

                int errorIndex = ADLX.intP_value(errorIndexPtr);
                Console.WriteLine("States error index is " + errorIndex);

                if (errorIndex == -1)
                    HasError(manual.SetFanTuningStates(stateList), $"Couldn't set fan speed to {speed}");

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
