using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class PerformanceMonitor : UnmanagedWrapper<IADLXPerformanceMonitoringServices>
    {
        public PerformanceMonitor( IADLXPerformanceMonitoringServices performanceMonitor ) : base( performanceMonitor )
        {

        }

        public void GetGPUMetrics(GPU gpu)
        {
            SWIGTYPE_p_p_adlx__IADLXGPUMetrics ptr = ADLX.new_metricsP_Ptr();
            UnmanagedInterface.GetCurrentGPUMetrics( gpu.UnmanagedInterface, ptr )
        }
    }
}