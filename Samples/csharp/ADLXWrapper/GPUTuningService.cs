﻿using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class GPUTuningService : ADLXInterfaceWrapper<IADLXGPUTuningServices>
    {
        public GPUTuningService(IADLXGPUTuningServices services) : base(services)
        {
        }

        public bool IsManualFanTuningSupported(GPU gpu)
        {
            var ptr = ADLX.new_boolP();
            NativeInterface.IsSupportedManualFanTuning(gpu.NativeInterface, ptr);
            var value = ADLX.boolP_value(ptr);

            return value;
        }

        public ManualFanTuning GetManualFanTuning(GPU gpu)
        {
            var ptr = ADLX.new_adlxInterfaceP_Ptr().DisposeWith(ADLX.delete_adlxInterfaceP_Ptr, Disposable);
            NativeInterface.GetManualFanTuning(gpu.NativeInterface, ptr);
            var @interface = ADLX.adlxInterfaceP_Ptr_value(ptr).DisposeWith(Disposable);

            return new ManualFanTuning(@interface);
        }
    }
}
