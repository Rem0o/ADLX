using ADLXWrapper.Bindings;
using System.Collections.Generic;

namespace ADLXWrapper
{
    public class SystemServices : Wrapper<IADLXSystem>
    {
        public SystemServices(IADLXSystem iADLXSystem) : base(iADLXSystem)
        {
        }

        public IReadOnlyList<GPU> GetGPUs()
        {
            var gpuListPtr = ADLX.new_gpuListP_Ptr().DisposeWith(ADLX.delete_gpuListP_Ptr, Disposable);
            NativeInterface.GetGPUs(gpuListPtr).ThrowIfError("Couldn't get GPU list");

            var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr).DisposeWith(Disposable);

            List<GPU> gpus = new List<GPU>();
            for (uint i = gpuList.Begin(); i < gpuList.End(); i++)
            {
                SWIGTYPE_p_p_adlx__IADLXGPU gpuPtr = ADLX.new_gpuP_Ptr().DisposeWith(ADLX.delete_gpuP_Ptr, Disposable);
                gpuList.At(i, gpuPtr).ThrowIfError($"Couldn't get gpu at index {i}");

                gpus.Add(new GPU(ADLX.gpuP_Ptr_value(gpuPtr)));
            }

            return gpus;
        }

        public GPUTuningService GetGPUTuningService()
        {
            var ptr = ADLX.new_gpuTuningP_Ptr().DisposeWith(ADLX.delete_gpuTuningP_Ptr, Disposable);
            NativeInterface.GetGPUTuningServices(ptr);
            var tuning = ADLX.gpuTuningP_Ptr_value(ptr);
            return new GPUTuningService(tuning);
        }

        public PerformanceMonitor GetPerformanceMonitor()
        {
            var ptr = ADLX.new_performanceP_Ptr().DisposeWith(ADLX.delete_performanceP_Ptr, Disposable);
            NativeInterface.GetPerformanceMonitoringServices(ptr);
            var performanceMonitor = ADLX.performanceP_Ptr_value(ptr);
            return new PerformanceMonitor(performanceMonitor);
        }
    }
}
