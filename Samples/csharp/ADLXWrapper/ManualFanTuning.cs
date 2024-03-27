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

            var boolP = ADLX.new_boolP().DisposeWith(ADLX.delete_boolP, Disposable);
            SupportsTargetFanSpeed = NativeInterface.IsSupportedTargetFanSpeed(boolP) == ADLX_RESULT.ADLX_OK && ADLX.boolP_value(boolP);
            SupportsZeroRPM = NativeInterface.IsSupportedZeroRPM(boolP) == ADLX_RESULT.ADLX_OK && ADLX.boolP_value(boolP);
        }

        public bool SupportsTargetFanSpeed { get; }

        public bool SupportsZeroRPM { get; }

        public Range SpeedRange { get; }

        public void SetTargetFanSpeed(int speedRPM)
        {
            NativeInterface.SetTargetFanSpeed(speedRPM)
                .ThrowIfError($"Couldn't set fan speed with targetFanSpeed {speedRPM}, range ({SpeedRange.Min},{SpeedRange.Max})");
        }

        public void SetFanTuningStates(int speedPercent)
        {
            for (int i = 0; i < _states.Length; i++)
                _states[i].SetFanSpeed(speedPercent);

            NativeInterface.SetFanTuningStates(_list)
                .ThrowIfError($"Couldn't set fan speed with tuning states {speedPercent} %");
        }

        public void SetFanTuningStates(int[] speedPercent)
        {
            for (int i = 0; i < _states.Length; i++)
                _states[i].SetFanSpeed(speedPercent[i]);

            NativeInterface.SetFanTuningStates(_list).ThrowIfError($"Couldn't set fan speed with tuning states {speedPercent} %");
        }

        public void SetFanTuningStates((int temp, int speed)[] states)
        {
            for (int i = 0; i < _states.Length; i++)
            {
                IADLXManualFanTuningState state = _states[i];
                state.SetTemperature(states[i].temp);
                state.SetFanSpeed(states[i].speed);
            }

            NativeInterface.SetFanTuningStates(_list).ThrowIfError($"Couldn't set fan speed with tuning states {states}");
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
