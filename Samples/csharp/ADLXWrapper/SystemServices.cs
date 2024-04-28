﻿using ADLXWrapper.Bindings;
using System.Collections.Generic;

namespace ADLXWrapper
{
    public class SystemServices : Wrapper<IADLXSystem>
    {
        private readonly ADLXHelper _helper;

        public SystemServices(IADLXSystem iADLXSystem, ADLXWrapper aDLXWrapper) : base(iADLXSystem)
        {
            _helper = aDLXWrapper.NativeInterface;
        }

        public IReadOnlyList<GPU> GetGPUs()
        {
            var gpuListPtr = ADLX.new_gpuListP_Ptr();
            NativeInterface.GetGPUs(gpuListPtr).ThrowIfError("Couldn't get GPU list");

            var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr).DisposeInterfaceWith(Disposable);

            List<GPU> gpus = new List<GPU>();
            for (uint i = gpuList.Begin(); i < gpuList.End(); i++)
            {
                SWIGTYPE_p_p_adlx__IADLXGPU gpuPtr = ADLX.new_gpuP_Ptr();
                gpuList.At(i, gpuPtr).ThrowIfError($"Couldn't get gpu at index {i}");
                gpus.Add(new GPU(ADLX.gpuP_Ptr_value(gpuPtr)));
            }

            return gpus;
        }

        public GPUTuningService GetGPUTuningService()
        {
            var ptr = ADLX.new_gpuTuningP_Ptr();
            NativeInterface.GetGPUTuningServices(ptr);
            var tuning = ADLX.gpuTuningP_Ptr_value(ptr);
            return new GPUTuningService(tuning, _helper);
        }

        public PerformanceMonitor GetPerformanceMonitor()
        {
            var ptr = ADLX.new_performanceP_Ptr();
            NativeInterface.GetPerformanceMonitoringServices(ptr);
            var performanceMonitor = ADLX.performanceP_Ptr_value(ptr);
            return new PerformanceMonitor(performanceMonitor, _helper);
        }
    }
}
