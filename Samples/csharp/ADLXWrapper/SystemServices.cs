using ADLXWrapper.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADLXWrapper
{
    public class ADLXWrapper : UnmanagedWrapper<ADLXHelper>
    {
        public ADLXWrapper() : base(new ADLXHelper())
        {
            UnmanagedInterface.Initialize().ThrowIfError("Couldn't initialize ADLX");
        }

        public SystemServices GetSystemServices()
        {
            return new SystemServices(UnmanagedInterface.GetSystemServices());
        }
    }

    public class SystemServices : UnmanagedWrapper<IADLXSystem>
    {

        public SystemServices(IADLXSystem iADLXSystem) : base(iADLXSystem)
        {
        }

        public IReadOnlyList<GPU> GetGPUs()
        {
            using (var disposable = new CompositeDisposable())
            {
                var gpuListPtr = ADLX.new_gpuListP_Ptr().DisposeWith(ADLX.delete_gpuListP_Ptr, disposable);
                UnmanagedInterface.GetGPUs(gpuListPtr).ThrowIfError("Couldn't get GPU list");

                var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr).DisposeWith(disposable);

                List<GPU> gpus = new List<GPU>();
                for (uint i = gpuList.Begin(); i < gpuList.End(); i++)
                {
                    SWIGTYPE_p_p_adlx__IADLXGPU gpuPtr = ADLX.new_gpuP_Ptr().DisposeWith(ADLX.delete_gpuP_Ptr, disposable);
                    gpuList.At(i, gpuPtr).ThrowIfError($"Couldn't get gpu at index {i}");

                    gpus.Add(new GPU(ADLX.gpuP_Ptr_value(gpuPtr)));
                }

                return gpus;
            }
        }

        public GPUTuningService GetGPUTuningService()
        {
            var ptr = ADLX.new_gpuTuningP_Ptr();
            UnmanagedInterface.GetGPUTuningServices(ptr);

            var tuning = ADLX.gpuTuningP_Ptr_value(ptr);
            ADLX.delete_gpuTuningP_Ptr(ptr);

            return new GPUTuningService(tuning);
        }
    }

    public abstract class UnmanagedWrapper<T> : IDisposable where T : IDisposable
    {
        protected readonly CompositeDisposable Disposable = new CompositeDisposable();
        internal T UnmanagedInterface;

        protected UnmanagedWrapper(T nativeInterface, ActionDisposable disposeUnmanagedRessources = null)
        {
            UnmanagedInterface = nativeInterface.DisposeWith(Disposable);
            if (disposeUnmanagedRessources != null)
            {
                Disposable.Add(disposeUnmanagedRessources);
            }
        }

        public void Dispose()
        {
            Disposable.Dispose();
        }
    }

    public class GPU : UnmanagedWrapper<IADLXGPU>
    {
        private IADLXGPU _gpu;

        public GPU(IADLXGPU gpu) : base(gpu)
        {
            using (var disposable = new CompositeDisposable())
            {
                var ptr = ADLX.new_stringP_Ptr().DisposeWith(ADLX.delete_stringP_Ptr, disposable);
                _gpu.Name(ptr).ThrowIfError("Couldn't get GPU name");

                Name = ADLX.stringP_Ptr_value(ptr);
            }
        }

        public string Name { get; }
    }

    public class GPUTuningService : UnmanagedWrapper<IADLXGPUTuningServices>
    {
        private readonly IADLXGPUTuningServices _services;

        public GPUTuningService(IADLXGPUTuningServices services) : base(services)
        {
            _services = services;
        }

        public ManualFanTuning GetManualFanTuning(GPU gpu)
        {
            var ptr = ADLX.new_adlxInterfaceP_Ptr();
            UnmanagedInterface.GetManualFanTuning(gpu.UnmanagedInterface, ptr);
            var @interface = ADLX.adlxInterfaceP_Ptr_value(ptr);
            var manual = ADLX.CastManualFanTuning(@interface);
            ADLX.delete_adlxInterfaceP_Ptr(ptr);
            return new ManualFanTuning(manual);
        }
    }

    public class Range
    {
        public Range(ADLX_IntRange range)
        {
            Min = range.minValue;
            Max = range.maxValue;
            Step = range.step;
        }

        public int Min { get; }
        public int Max { get; }
        public int Step { get; }
    }

    public class ManualFanTuning : UnmanagedWrapper<IADLXManualFanTuning>
    {
        private IADLXManualFanTuningStateList _list;
        private IADLXManualFanTuningState[] _states;
       

        public ManualFanTuning(IADLXManualFanTuning fanTuning) : base(fanTuning)
        {
            using (var constructorDisposable = new CompositeDisposable())
            {
                var listPtr = ADLX.new_fanTuningStateListP_Ptr().DisposeWith(ADLX.delete_fanTuningStateListP_Ptr, constructorDisposable);
                fanTuning.GetFanTuningStates(listPtr);
                _list = ADLX.fanTuningStateListP_Ptr_value(listPtr).DisposeWith(Disposable);

                _states = Enumerable.Range((int)_list.Begin(), (int)(_list.End() - _list.Begin())).Select(i =>
                {
                    var statePtr = ADLX.new_fanTuningStateP_Ptr().DisposeWith(ADLX.delete_fanTuningStateP_Ptr, constructorDisposable);
                    _list.At((uint)i, statePtr).ThrowIfError($"Couldn't get state {i}");
                    return ADLX.fanTuningStateP_Ptr_value(statePtr).DisposeWith(Disposable);
                }).ToArray();

                var speedRangePtr = ADLX.new_adlx_intRangeP().DisposeWith(ADLX.delete_adlx_intRangeP, constructorDisposable);
                var tempRangePtr = ADLX.new_adlx_intRangeP().DisposeWith(ADLX.delete_adlx_intRangeP, constructorDisposable);
                fanTuning.GetFanTuningRanges(speedRangePtr, tempRangePtr).ThrowIfError("Couldn't get fan speed range.");

                using (ADLX_IntRange speedRange = ADLX.adlx_intRangeP_value(speedRangePtr))
                    SpeedRange = new Range(speedRange);
            }
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
