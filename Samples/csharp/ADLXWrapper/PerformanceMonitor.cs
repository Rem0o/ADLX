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
            var ptr = ADLX.new_metricsP_Ptr().DisposeWith(ADLX.delete_metricsP_Ptr, Disposable);
            UnmanagedInterface.GetCurrentGPUMetrics( gpu.UnmanagedInterface, ptr );
            return new GPUMetrics( ADLX.metricsP_Ptr_value(ptr));
        }
    }
}