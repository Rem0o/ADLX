using ADLXWrapper.Bindings;
using System.Linq;

namespace ADLXWrapper
{
    public class ManualFanTuning : ADLXInterfaceWrapper<IADLXManualFanTuning>
    {
        private IADLXManualFanTuningStateList _list;
        private IADLXManualFanTuningState[] _states;
        private (int t, int s)[] _resetList;
        private SWIGTYPE_p_int _intPtr;

        public ManualFanTuning(IADLXInterface @interface) : base(ADLX.CastManualFanTuning(@interface))
        {
            _intPtr = ADLX.new_intP();
            var listPtr = ADLX.new_fanTuningStateListP_Ptr();
            NativeInterface.GetFanTuningStates(listPtr).ThrowIfError("Couldn't get fan tuning states");
            _list = ADLX.fanTuningStateListP_Ptr_value(listPtr).DisposeInterfaceWith(Disposable);

            _states = Enumerable.Range((int)_list.Begin(), (int)(_list.End() - _list.Begin())).Select(i =>
            {
                var statePtr = ADLX.new_fanTuningStateP_Ptr();
                _list.At((uint)i, statePtr).ThrowIfError($"Couldn't get state {i}");
                return ADLX.fanTuningStateP_Ptr_value(statePtr).DisposeInterfaceWith(Disposable);
            }).ToArray();

            _resetList = _states.Select(x =>
            {
                x.GetTemperature(_intPtr);
                var t = ADLX.intP_value(_intPtr);
                x.GetFanSpeed(_intPtr);
                var s = ADLX.intP_value(_intPtr);
                return (t, s);
            }).ToArray();

            var speedRangePtr = ADLX.new_adlx_intRangeP();
            var tempRangePtr = ADLX.new_adlx_intRangeP();
            NativeInterface.GetFanTuningRanges(speedRangePtr, tempRangePtr).ThrowIfError("Couldn't get fan speed range.");

            using (ADLX_IntRange speedRange = ADLX.adlx_intRangeP_value(speedRangePtr))
                SpeedRange = new Range(speedRange);

            var boolP = ADLX.new_boolP();
            SupportsTargetFanSpeed = NativeInterface.IsSupportedTargetFanSpeed(boolP) == ADLX_RESULT.ADLX_OK && ADLX.boolP_value(boolP);
            SupportsZeroRPM = NativeInterface.IsSupportedZeroRPM(boolP) == ADLX_RESULT.ADLX_OK && ADLX.boolP_value(boolP);
        }

        public bool SupportsTargetFanSpeed { get; }

        public bool SupportsZeroRPM { get; }

        public Range SpeedRange { get; }

        public void Reset()
        {
            SetFanTuningStates(_resetList);
        }

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

        public void SetFanTuningStates2(int speedPercent)
        {
            if (_list.End() == 5)
            {
                var statePtr = ADLX.new_fanTuningStateP_Ptr();
                using (var compositeDisposable = new CompositeDisposable())
                {
                    foreach (var i in Enumerable.Range(2, 3))
                    {
                        _list.At((uint)i, statePtr);
                        var state = ADLX.fanTuningStateP_Ptr_value(statePtr).DisposeInterfaceWith(compositeDisposable);
                        var temp = 90 + ((i - 2) * 5);
                        state.SetTemperature(temp);
                        state.SetFanSpeed(100);
                    }

                    compositeDisposable.Dispose();
                }

                _list.Remove_Back();
                _list.Remove_Back();
                _list.Remove_Back();
            }

            ADLXHelper.SetSpeed(speedPercent, this.NativeInterface, _list).ThrowIfError($"Couldn't set fan speed with tuning states {speedPercent} %");
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
    }
}
