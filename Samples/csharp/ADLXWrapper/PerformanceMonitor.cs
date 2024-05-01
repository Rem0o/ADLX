using ADLXWrapper.Bindings;
using System;

namespace ADLXWrapper
{

    public class PerformanceMonitor : ADLXInterfaceWrapper<IADLXPerformanceMonitoringServices>
    {
        private readonly ADLXHelper _helper;
        private SWIGTYPE_p_p_adlx__IADLXGPUMetrics _metricPtr;
        private SWIGTYPE_p_double _doublePtr;
        private SWIGTYPE_p_int _intPtr;
        private readonly SWIGTYPE_p_p_adlx__IADLXGPUMetricsList _metricsList;

        public PerformanceMonitor(IADLXPerformanceMonitoringServices performanceMonitor, ADLXHelper helper) : base(performanceMonitor)
        {
            _metricPtr = ADLX.new_metricsP_Ptr().DisposeWith(ADLX.delete_metricsP_Ptr, Disposable);
            _doublePtr = ADLX.new_doubleP().DisposeWith(ADLX.delete_doubleP, Disposable);
            _intPtr = ADLX.new_intP().DisposeWith(ADLX.delete_intP, Disposable);
            _metricsList = ADLX.new_gpuMetricsListP_Ptr().DisposeWith(ADLX.delete_gpuMetricsListP_Ptr, Disposable);
            _helper = helper;
        }

        public IDisposable StartTracking(int samplingIntervalMs, int? maxHistorySize = null)
        {
            NativeInterface.SetSamplingInterval(samplingIntervalMs).ThrowIfError("Sampling interval");
            if (maxHistorySize.HasValue)
                NativeInterface.SetMaxPerformanceMetricsHistorySize(maxHistorySize.Value).ThrowIfError("History Size");

            NativeInterface.StartPerformanceMetricsTracking().ThrowIfError("Start performance tracking");

            return new ActionDisposable(() => NativeInterface.StopPerformanceMetricsTracking().ThrowIfError("Couldn't stop performance metrics tracking"));
        }

        public GPUMetrics GetGPUMetricsFromTracking(GPU gpu)
        {
            NativeInterface.GetGPUMetricsHistory(gpu.NativeInterface, 1, 0, _metricsList).ThrowIfError("Couldn't get GPU metrics history");
            var list = ADLX.gpuMetricsListP_Ptr_value(_metricsList).DisposeInterfaceWith(Disposable);

            list.At(0, _metricPtr).ThrowIfError("GPUMetricsHistory list at");
            return new GPUMetrics(_metricPtr, _intPtr, _doublePtr);
        }

        public GPUMetrics GetGPUMetrics(GPU gpu)
        {
            NativeInterface.GetCurrentGPUMetrics(gpu.NativeInterface, _metricPtr);
            return new GPUMetrics(_metricPtr, _intPtr, _doublePtr);
        }

        public GPUMetricsStruct GetGPUMetricsStruct(GPU gpu)
        {
            GPUMetricsStruct metrics = default;
            _helper.GetCurrentMetrics(NativeInterface, gpu.NativeInterface, ref metrics);

            return metrics;
        }

        public GPUMetricsStruct GetGPUMetricsStructFromTracking(GPU gpu)
        {
            GPUMetricsStruct metrics = default;
            _helper.GetLatestMetricsFromTracking(NativeInterface, gpu.NativeInterface, ref metrics);

            return metrics;
        }
    }
}