﻿using System;

namespace ADLXWrapper.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize ADLX with ADLXHelper
            ADLXHelper help = new ADLXHelper();
            if (HasError(help.Initialize(), "Couldn't initialize ADLX"))
            {
                Console.ReadKey();
                return;
            }

            var systemServices = help.GetSystemServices();

            var gpuListPtr = ADLX.new_gpuListP_Ptr();
            if (HasError(systemServices.GetGPUs(gpuListPtr), "Couldn't get GPU list"))
                return;

            var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);

            var servicePtr = ADLX.new_gpuTuningP_Ptr();
            if (HasError(systemServices.GetGPUTuningServices(servicePtr), "Couldn't get GPU tuning"))
                return;

            var tuningServices = ADLX.gpuTuningP_Ptr_value(servicePtr);

            var index = gpuList.Begin();
            var gpuPtr = ADLX.new_gpuP_Ptr();
            if (HasError(gpuList.At(index, gpuPtr), "Couldn't get GPU"))
                return;

            var gpu = ADLX.gpuP_Ptr_value(gpuPtr);

            var interfacePtr = ADLX.new_adlxInterfaceP_Ptr();
            if (HasError(tuningServices.GetManualFanTuning(gpu, interfacePtr), "Couldn't get interface"))
                return;

            var @interface = ADLX.adlxInterfaceP_Ptr_value(interfacePtr);
            var manual = ADLX.CastManualFanTuning(@interface);

            var boolPtr = ADLX.new_boolP();
            if (HasError(manual.IsSupportedTargetFanSpeed(boolPtr), "Could not check for is supported")) 
                return;

            var isSupported = ADLX.boolP_value(boolPtr);

            Console.WriteLine($"Set target fan speed is supported: {isSupported}");

            if (isSupported)
            {
                manual.SetTargetFanSpeed(50);
                Console.WriteLine("Setting fan speed to 50");
            }

            var stateListPtre = ADLX.new_fanTuningStateListP_Ptr();
            manual.GetFanTuningStates(stateListPtre);
            var stateList = ADLX.fanTuningStateListP_Ptr_value(stateListPtre);

            for( uint i = stateList.Begin(); i < stateList.End(); i++ )
            {
                var statePtr = ADLX.new_fanTuningStateP_Ptr();
                stateList.At(i, statePtr);
                var state = ADLX.fanTuningStateP_Ptr_value(statePtr);

                state.SetFanSpeed(50);
                state.Dispose();
            }

            var errorIndexPtr = ADLX.new_intP();
            if ( HasError(manual.IsValidFanTuningStates(stateList, errorIndexPtr), "Couldn't validate states" ) )
            {
                return;
            }

            int errorIndex = ADLX.intP_value( errorIndexPtr );
            Console.WriteLine("States error index is " + errorIndex );

            if (errorIndex == -1 )
                manual.SetFanTuningStates(stateList);
            
            var performancePtr = ADLX.new_performanceP_Ptr();
            if (HasError(systemServices.GetPerformanceMonitoringServices(performancePtr), "Couldn't get performance monitor"))
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
            Console.WriteLine($"Temp is {temp}");

            var intPtr = ADLX.new_intP();
            if (HasError(metrics.GPUFanSpeed(intPtr), "Couldn't get fan speed"))
                return;

            var fanSpeed = ADLX.intP_value(intPtr);
            Console.WriteLine($"Fan speed is {fanSpeed}");

            metrics.Dispose();
            performance.Dispose();
            stateList.Dispose();
            manual.Dispose();
            @interface.Dispose();
            gpu.Dispose();
            gpuList.Dispose();
            tuningServices.Dispose();
            systemServices.Dispose();
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