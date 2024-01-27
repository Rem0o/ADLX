﻿using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class GPU : ADLXInterfaceWrapper<IADLXGPU>
    {
        public GPU(IADLXGPU gpu) : base(gpu)
        {
            var ptr = ADLX.new_stringP_Ptr().DisposeWith(ADLX.delete_stringP_Ptr, Disposable);
            var uniqueIdPtr = ADLX.new_intP().DisposeWith(ADLX.delete_intP, Disposable);
            NativeInterface.Name(ptr).ThrowIfError("Couldn't get GPU name");
            Name = ADLX.stringP_Ptr_value(ptr);
            NativeInterface.UniqueId(uniqueIdPtr).ThrowIfError("Couldn't get Unique ID");
            UniqueId = ADLX.intP_value(uniqueIdPtr);
        }

        public string Name { get; }

        public int UniqueId { get; }
    }
}
