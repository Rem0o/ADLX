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
            _metricPtr = ADLX.new_metricsP_Ptr();
            _doublePtr = ADLX.new_doubleP();
            _intPtr = ADLX.new_intP();
        }

        public GPUMetrics GetGPUMetrics(GPU gpu)
        {
            NativeInterface.GetCurrentGPUMetrics(gpu.NativeInterface, _metricPtr);
            return new GPUMetrics(_metricPtr, _intPtr, _doublePtr);
        }
    }
}