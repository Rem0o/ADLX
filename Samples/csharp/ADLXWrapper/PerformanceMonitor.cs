using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class PerformanceMonitor : UnmanagedWrapper<IADLXPerformanceMonitoringServices>
    {
        public PerformanceMonitor( IADLXPerformanceMonitoringServices performanceMonitor ) : base( performanceMonitor )
        {

        }

        public GPUMetrics GetGPUMetrics(GPU gpu)
        {
            var ptr = ADLX.new_metricsP_Ptr();
            UnmanagedInterface.GetCurrentGPUMetrics( gpu.UnmanagedInterface, ptr );
            return new GPUMetrics( ADLX.metricsP_Ptr_value(ptr));
        }
    }
}