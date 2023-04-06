using ADLXWrapper.Bindings;
using System.Linq;

namespace ADLXWrapper
{
    public class ManualFanTuning : ADLXInterfaceWrapper<IADLXManualFanTuning>
    {
        private IADLXManualFanTuningStateList _list;
        private IADLXManualFanTuningState[] _states;
        private SWIGTYPE_p_int _intPtr = ADLX.new_intP();
        private readonly IADLXInterface _interface;

        public ManualFanTuning(IADLXInterface @interface) : base(ADLX.CastManualFanTuning(@interface))
        {
            _intPtr.DisposeWith(ADLX.delete_intP, Disposable);
            var listPtr = ADLX.new_fanTuningStateListP_Ptr().DisposeWith(ADLX.delete_fanTuningStateListP_Ptr, Disposable);
            NativeInterface.GetFanTuningStates(listPtr);
            _list = ADLX.fanTuningStateListP_Ptr_value(listPtr).DisposeWith(Disposable);

            _states = Enumerable.Range((int)_list.Begin(), (int)(_list.End() - _list.Begin())).Select(i =>
            {
                var statePtr = ADLX.new_fanTuningStateP_Ptr().DisposeWith(ADLX.delete_fanTuningStateP_Ptr, Disposable);
                _list.At((uint)i, statePtr).ThrowIfError($"Couldn't get state {i}");
                return ADLX.fanTuningStateP_Ptr_value(statePtr).DisposeWith(Disposable);
            }).ToArray();

            var speedRangePtr = ADLX.new_adlx_intRangeP().DisposeWith(ADLX.delete_adlx_intRangeP, Disposable);
            var tempRangePtr = ADLX.new_adlx_intRangeP().DisposeWith(ADLX.delete_adlx_intRangeP, Disposable);
            NativeInterface.GetFanTuningRanges(speedRangePtr, tempRangePtr).ThrowIfError("Couldn't get fan speed range.");

            using (ADLX_IntRange speedRange = ADLX.adlx_intRangeP_value(speedRangePtr))
                SpeedRange = new Range(speedRange);

            _interface = @interface;
        }

        public Range SpeedRange { get; }

        public void SetFanSpeed(int speed)
        {
            for (int i = 0; i < _states.Length; i++)
                _states[i].SetFanSpeed(speed);

            NativeInterface.SetFanTuningStates(_list).ThrowIfError($"Couldn't set fan speed {speed}");
        }

        public void SetZeroRPM(bool enabled)
        {
            NativeInterface.SetZeroRPMState(enabled);
        }

        public int[] GetCurrentState()
        {
            var states = _states.Select(x =>
            {

                x.GetFanSpeed(_intPtr).ThrowIfError("Couldn't get control state");
                return ADLX.intP_value(_intPtr);
            });

            return states.ToArray();
        }

        public bool GetZeroRPMState()
        {
            var ptr = ADLX.new_boolP();
            NativeInterface.GetZeroRPMState(ptr).ThrowIfError("Couldn't get zero RPM state");

            return ADLX.boolP_value(ptr);
        }

        public override void Dispose()
        {
            _interface.Release();
            base.Dispose();
        }
    }
}
