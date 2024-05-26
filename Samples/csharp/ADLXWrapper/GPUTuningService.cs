using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class GPUTuningService : ADLXInterfaceWrapper<IADLXGPUTuningServices>
    {
        private readonly ADLXHelper _helper;

        public GPUTuningService(IADLXGPUTuningServices services, ADLXHelper helper) : base(services) => _helper = helper;

        public bool IsManualFanTuningSupported(GPU gpu)
        {
            var isSupportedPtr = ADLX.new_boolP();
            NativeInterface.IsSupportedManualFanTuning(gpu.NativeInterface, isSupportedPtr);
            var value = ADLX.boolP_value(isSupportedPtr);
            ADLX.delete_boolP(isSupportedPtr);

            return value;
        }

        public ManualFanTuning GetManualFanTuning(GPU gpu)
        {
            var interfacePtr = ADLX.new_adlxInterfaceP_Ptr();
            NativeInterface.GetManualFanTuning(gpu.NativeInterface, interfacePtr);
            var @interface = ADLX.adlxInterfaceP_Ptr_value(interfacePtr);
            ADLX.delete_adlxInterfaceP_Ptr(interfacePtr);

            return new ManualFanTuning(@interface, _helper);
        }
    }
}
