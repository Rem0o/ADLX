using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class GPUTuningService : UnmanagedWrapper<IADLXGPUTuningServices>
    {
        public GPUTuningService(IADLXGPUTuningServices services) : base(services)
        {
        }

        public bool IsManualFanTuningSupported(GPU gpu)
        {
            var ptr = ADLX.new_boolP();
            UnmanagedInterface.IsSupportedManualFanTuning(gpu.UnmanagedInterface, ptr);
            var value = ADLX.boolP_value(ptr);

            return value;
        }

        public ManualFanTuning GetManualFanTuning(GPU gpu)
        {
            var ptr = ADLX.new_adlxInterfaceP_Ptr().DisposeWith(ADLX.delete_adlxInterfaceP_Ptr, Disposable);
            UnmanagedInterface.GetManualFanTuning(gpu.UnmanagedInterface, ptr);
            var @interface = ADLX.adlxInterfaceP_Ptr_value(ptr).DisposeWith(Disposable);

            return new ManualFanTuning(@interface);
        }
    }
}
