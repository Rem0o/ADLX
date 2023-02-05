using ADLXWrapper.Bindings;
using System.Collections.Generic;

namespace ADLXWrapper
{
    public class SystemServices : UnmanagedWrapper<IADLXSystem>
    {
        public SystemServices(IADLXSystem iADLXSystem) : base(iADLXSystem)
        {
        }

        public IReadOnlyList<GPU> GetGPUs()
        {
            using (var disposable = new CompositeDisposable())
            {
                var gpuListPtr = ADLX.new_gpuListP_Ptr().DisposeWith(ADLX.delete_gpuListP_Ptr, disposable);
                UnmanagedInterface.GetGPUs(gpuListPtr).ThrowIfError("Couldn't get GPU list");

                var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr).DisposeWith(disposable);

                List<GPU> gpus = new List<GPU>();
                for (uint i = gpuList.Begin(); i < gpuList.End(); i++)
                {
                    SWIGTYPE_p_p_adlx__IADLXGPU gpuPtr = ADLX.new_gpuP_Ptr().DisposeWith(ADLX.delete_gpuP_Ptr, disposable);
                    gpuList.At(i, gpuPtr).ThrowIfError($"Couldn't get gpu at index {i}");

                    gpus.Add(new GPU(ADLX.gpuP_Ptr_value(gpuPtr)));
                }

                return gpus;
            }
        }

        public GPUTuningService GetGPUTuningService()
        {
            var ptr = ADLX.new_gpuTuningP_Ptr();
            UnmanagedInterface.GetGPUTuningServices(ptr);

            var tuning = ADLX.gpuTuningP_Ptr_value(ptr);
            ADLX.delete_gpuTuningP_Ptr(ptr);

            return new GPUTuningService(tuning);
        }
    }
}
