using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class PerformanceMonitor : ADLXInterfaceWrapper<IADLXPerformanceMonitoringServices>
    {
        private SWIGTYPE_p_p_adlx__IADLXGPUMetrics _metricPtr;
        private SWIGTYPE_p_double _doublePtr;
        private SWIGTYPE_p_int _intPtr;

        public PerformanceMonitor(IADLXPerformanceMonitoringServices performanceMonitor) : base(performanceMonitor)
        {
            _metricPtr = ADLX.new_metricsP_Ptr().DisposeWith(ADLX.delete_metricsP_Ptr, Disposable);
            _doublePtr = ADLX.new_doubleP().DisposeWith(ADLX.delete_doubleP, Disposable);
            _intPtr = ADLX.new_intP().DisposeWith(ADLX.delete_intP, Disposable);
        }

        public GPUMetrics GetGPUMetrics(GPU gpu)
        {
            NativeInterface.GetCurrentGPUMetrics(gpu.NativeInterface, _metricPtr);
            return new GPUMetrics(_metricPtr, _intPtr, _doublePtr);
        }
    }
}