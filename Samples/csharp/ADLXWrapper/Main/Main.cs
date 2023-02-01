using System;

namespace ADLXWrapper
{
    internal class Main
    {

        public void lol()
        {
            // Initialize ADLX with ADLXHelper
            ADLXHelper help = new ADLXHelper();
            if (HasError(help.Initialize(), "Couldn't initialize ADLX"))
                return;

            var sys = help.GetSystemServices();

            var g = ADLX.new_gpuListP_Ptr();
            if (HasError(sys.GetGPUs(g), "Couldn't get GPU list"))
                return;

            var gpuList = ADLX.gpuListP_Ptr_value(g);

            var servicePtr = ADLX.new_gpuTuningP_Ptr();
            if (HasError(sys.GetGPUTuningServices(servicePtr), "Couldn't get GPU tuning"))
                return;

            var services = ADLX.gpuTuningP_Ptr_value(servicePtr);

            var index = gpuList.Begin();
            var gpuPtr = ADLX.new_gpuP_Ptr();
            if (HasError(gpuList.At(index, gpuPtr), "Couldn't get GPU"))
                return;

            var gpu = ADLX.gpuP_Ptr_value(gpuPtr);

            var interfacePtr = ADLX.new_adlxInterfaceP_Ptr();
            if (HasError(services.GetManualFanTuning(gpu, interfacePtr), "Couldn't get interface"))
                return;

            var manual = ADLX.CastManualFanTuning(ADLX.adlxInterfaceP_Ptr_value(interfacePtr));

            manual.SetTargetFanSpeed(1000);

            var performancePtr = ADLX.new_performanceP_Ptr();
            if (HasError(sys.GetPerformanceMonitoringServices(performancePtr), "Couldn't get performance monitor"))
                return;

            var performance = ADLX.performanceP_Ptr_value(performancePtr);
            var metricsPtr = ADLX.new_metricsP_Ptr();
            if (HasError(performance.GetCurrentGPUMetrics(gpu, metricsPtr), "Couldn't get metrics"))
                return;

            var metrics = ADLX.metricsP_Ptr_value(metricsPtr);

            var doublePtr = ADLX.new_doubleP();
            if (HasError(metrics.GPUTemperature(doublePtr), "Couldn't get temp"))
                return;

            var temp = ADLX.doubleP_value(doublePtr);

            var intPtr = ADLX.new_intP();
            if (HasError(metrics.GPUFanSpeed(intPtr), "Couldn't get fan speed"))
                return;

            var fanSpeed = ADLX.intP_value(intPtr);
        }

        private bool HasError(ADLX_RESULT result, string message)
        {
            if (result == ADLX_RESULT.ADLX_OK)
                return false;

            Console.WriteLine(message);

            return true;
        }
    }
}
