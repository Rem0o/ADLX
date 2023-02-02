using ADLXWrapper.Bindings;
using System;
using System.Threading.Tasks;

namespace ADLXWrapper.TestConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var dispose = new CompositeDisposable())
            {
                var adlxHelper = new ADLXHelper().Using(dispose);
                if (HasError(adlxHelper.Initialize(), "Couldn't initialize ADLX"))
                    return;

                var systemServices = adlxHelper.GetSystemServices().Using(dispose);

                var gpuListPtr = ADLX.new_gpuListP_Ptr();
                if (HasError(systemServices.GetGPUs(gpuListPtr), "Couldn't get GPU list"))
                    return;

                var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr).Using(dispose);

                var servicePtr = ADLX.new_gpuTuningP_Ptr();
                if (HasError(systemServices.GetGPUTuningServices(servicePtr), "Couldn't get GPU tuning"))
                    return;

                var tuningServices = ADLX.gpuTuningP_Ptr_value(servicePtr).Using(dispose);

                var index = gpuList.Begin();
                var gpuPtr = ADLX.new_gpuP_Ptr();
                if (HasError(gpuList.At(index, gpuPtr), "Couldn't get GPU"))
                    return;

                var gpu = ADLX.gpuP_Ptr_value(gpuPtr).Using(dispose);

                var interfacePtr = ADLX.new_adlxInterfaceP_Ptr();

                if (HasError(tuningServices.GetManualFanTuning(gpu, interfacePtr), "Couldn't get interface"))
                    return;

                var @interface = ADLX.adlxInterfaceP_Ptr_value(interfacePtr).Using(dispose);
                var manual = ADLX.CastManualFanTuning(@interface).Using(dispose);

                var boolPtr = ADLX.new_boolP();
                if (HasError(manual.IsSupportedTargetFanSpeed(boolPtr), "Could not check for is supported"))
                    return;

                var isSupported = ADLX.boolP_value(boolPtr);

                Console.WriteLine($"Set target fan speed is supported: {isSupported}");

                var targetFanSpeedPtr = ADLX.new_intP();
                if (!HasError(manual.GetTargetFanSpeed(targetFanSpeedPtr), "Could not get target Fan speed"))
                    Console.WriteLine($"Current target fan speed is: {ADLX.intP_value(targetFanSpeedPtr)}");


                if (isSupported)
                {
                    manual.SetTargetFanSpeed(50);
                    Console.WriteLine("Setting fan speed to 50");
                }

                var zeroRpmPtr = ADLX.new_boolP();
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

                var performancePtr = ADLX.new_performanceP_Ptr();
                if (HasError(systemServices.GetPerformanceMonitoringServices(performancePtr), "Couldn't get performance monitor"))
                    return;

                var performance = ADLX.performanceP_Ptr_value(performancePtr).Using(dispose);
                var metricsPtr = ADLX.new_metricsP_Ptr();
                if (HasError(performance.GetCurrentGPUMetrics(gpu, metricsPtr), "Couldn't get metrics"))
                    return;

                IADLXGPUMetrics metrics = ADLX.metricsP_Ptr_value(metricsPtr);

                var doublePtr = ADLX.new_doubleP();
                if (HasError(metrics.GPUTemperature(doublePtr), "Couldn't get temp"))
                    return;

                var temp = ADLX.doubleP_value(doublePtr);
                Console.WriteLine($"Temp is {temp}");

                if (HasError(metrics.GPUHotspotTemperature(doublePtr), "Couldn't get hotspot temp"))
                    return;

                temp = ADLX.doubleP_value(doublePtr);
                Console.WriteLine($"Hotspot Temp is {temp}");

                var intPtr = ADLX.new_intP();
                if (HasError(metrics.GPUFanSpeed(intPtr), "Couldn't get fan speed"))
                    return;

                var fanSpeed = ADLX.intP_value(intPtr);
                Console.WriteLine($"Fan speed is {fanSpeed}");
            }
        }

        private static void SetFanSpeed(IADLXManualFanTuning manual, int v)
        {
            var stateListPtr = ADLX.new_fanTuningStateListP_Ptr();
            manual.GetFanTuningStates(stateListPtr);
            var stateList = ADLX.fanTuningStateListP_Ptr_value(stateListPtr);

            for (uint i = stateList.Begin(); i < stateList.End(); i++)
            {
                var statePtr = ADLX.new_fanTuningStateP_Ptr();
                stateList.At(i, statePtr);
                var state = ADLX.fanTuningStateP_Ptr_value(statePtr);

                state.SetFanSpeed(v);
                state.Dispose();
            }

            var errorIndexPtr = ADLX.new_intP();
            HasError(manual.IsValidFanTuningStates(stateList, errorIndexPtr), "Couldn't validate states");

            int errorIndex = ADLX.intP_value(errorIndexPtr);
            Console.WriteLine("States error index is " + errorIndex);

            if (errorIndex == -1)
                HasError(manual.SetFanTuningStates(stateList), $"Couldn't set fan speed to {v}");

            stateList.Dispose();
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
