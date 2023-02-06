using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class GPUTuningService : UnmanagedWrapper<IADLXGPUTuningServices>
    {
        private readonly IADLXGPUTuningServices _services;

        public GPUTuningService(IADLXGPUTuningServices services) : base(services)
        {
            _services = services;
        }

        public ManualFanTuning GetManualFanTuning(GPU gpu)
        {
            var ptr = ADLX.new_adlxInterfaceP_Ptr();
            UnmanagedInterface.GetManualFanTuning(gpu.UnmanagedInterface, ptr);
            var @interface = ADLX.adlxInterfaceP_Ptr_value(ptr);
            var manual = ADLX.CastManualFanTuning(@interface);
            ADLX.delete_adlxInterfaceP_Ptr(ptr);

            return new ManualFanTuning(manual);
        }
    }
}
