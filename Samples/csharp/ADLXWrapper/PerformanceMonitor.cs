using ADLXWrapper.Bindings;

namespace ADLXWrapper
{

    public class PerformanceMonitor : ADLXInterfaceWrapper<IADLXPerformanceMonitoringServices>
    {
        private readonly ADLXHelper _helper;
        private SWIGTYPE_p_p_adlx__IADLXGPUMetrics _metricPtr;
        private SWIGTYPE_p_double _doublePtr;
        private SWIGTYPE_p_int _intPtr;

        public PerformanceMonitor(IADLXPerformanceMonitoringServices performanceMonitor, ADLXHelper helper) : base(performanceMonitor)
        {
            _metricPtr = ADLX.new_metricsP_Ptr();
            _doublePtr = ADLX.new_doubleP();
            _intPtr = ADLX.new_intP();
            _helper = helper;
        }

        public GPUMetrics GetGPUMetrics(GPU gpu)
        {
            NativeInterface.GetCurrentGPUMetrics(gpu.NativeInterface, _metricPtr);
            return new GPUMetrics(_metricPtr, _intPtr, _doublePtr);
        }
         
        public GPUMetricsStruct GetGPUMetricsStruct(GPU gpu)
        {
            GPUMetricsStruct metrics = default;
            _helper.GetCurrentMetrics(this.NativeInterface, gpu.NativeInterface, ref metrics);

            return metrics;
        }
    }
}