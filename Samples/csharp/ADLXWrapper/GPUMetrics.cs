using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class GPUMetrics : UnmanagedWrapper<IADLXGPUMetrics>
    {
        private SWIGTYPE_p_int _intPtr;
        private SWIGTYPE_p_double _doublePtr;

        public GPUMetrics( IADLXGPUMetrics gpuMetrics ) : base( gpuMetrics )
        {
            _intPtr = ADLX.new_intP().DisposeWith( ADLX.delete_intP, Disposable );
            _doublePtr = ADLX.new_doubleP().DisposeWith( ADLX.delete_doubleP, Disposable );
        }

        public int GetFanSpeed()
        {
            lock ( _intPtr )
            {
                UnmanagedInterface.GPUFanSpeed( _intPtr );
                return ADLX.intP_value( _intPtr );
            }
        }

        public double GetHotspotTemperature()
        {
            lock ( _doublePtr )
            {
                UnmanagedInterface.GPUHotspotTemperature( _doublePtr );
                return ADLX.doubleP_value( _doublePtr );
            }
        }

        public double GetGPUTemperature()
        {
            lock ( _doublePtr )
            {
                UnmanagedInterface.GPUTemperature( _doublePtr );
                return ADLX.doubleP_value( _doublePtr );
            }
        }
    }
}