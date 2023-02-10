using ADLXWrapper.Bindings;
using System.Linq;

namespace ADLXWrapper
{
    public class ManualFanTuning : UnmanagedWrapper<IADLXManualFanTuning>
    {
        private IADLXManualFanTuningStateList _list;
        private IADLXManualFanTuningState[] _states;

        public ManualFanTuning(IADLXInterface @interface) : base(ADLX.CastManualFanTuning(@interface))
        {
            @interface.DisposeWith(Disposable);
            var listPtr = ADLX.new_fanTuningStateListP_Ptr().DisposeWith(ADLX.delete_fanTuningStateListP_Ptr, Disposable);
            UnmanagedInterface.GetFanTuningStates(listPtr);
            _list = ADLX.fanTuningStateListP_Ptr_value(listPtr).DisposeWith(Disposable);

            _states = Enumerable.Range((int)_list.Begin(), (int)(_list.End() - _list.Begin())).Select(i =>
            {
                var statePtr = ADLX.new_fanTuningStateP_Ptr().DisposeWith(ADLX.delete_fanTuningStateP_Ptr, Disposable);
                _list.At((uint)i, statePtr).ThrowIfError($"Couldn't get state {i}");
                return ADLX.fanTuningStateP_Ptr_value(statePtr).DisposeWith(Disposable);
            }).ToArray();

            var speedRangePtr = ADLX.new_adlx_intRangeP().DisposeWith(ADLX.delete_adlx_intRangeP, Disposable);
            var tempRangePtr = ADLX.new_adlx_intRangeP().DisposeWith(ADLX.delete_adlx_intRangeP, Disposable);
            UnmanagedInterface.GetFanTuningRanges(speedRangePtr, tempRangePtr).ThrowIfError("Couldn't get fan speed range.");

            using (ADLX_IntRange speedRange = ADLX.adlx_intRangeP_value(speedRangePtr))
                SpeedRange = new Range(speedRange);
        }

        public Range SpeedRange { get; }

        public void SetFanSpeed(int speed)
        {
            for (int i = 0; i < _states.Length; i++)
                _states[i].SetFanSpeed(speed);

            UnmanagedInterface.SetFanTuningStates(_list).ThrowIfError($"Couldn't set fan speed {speed}");
        }
    }
}
