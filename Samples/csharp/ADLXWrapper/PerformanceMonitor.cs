using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class PerformanceMonitor : ADLXInterfaceWrapper<IADLXPerformanceMonitoringServices>
    {
        private SWIGTYPE_p_p_adlx__IADLXGPUMetrics _ptr;

        public PerformanceMonitor(IADLXPerformanceMonitoringServices performanceMonitor) : base(performanceMonitor)
        {
            _ptr = ADLX.new_metricsP_Ptr().DisposeWith(ADLX.delete_metricsP_Ptr, Disposable);
        }

        public GPUMetrics GetGPUMetrics(GPU gpu)
        {
            NativeInterface.GetCurrentGPUMetrics(gpu.NativeInterface, _ptr);
            return new GPUMetrics(ADLX.metricsP_Ptr_value(_ptr));
        }
    }
}