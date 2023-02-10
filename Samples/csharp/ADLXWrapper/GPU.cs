using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class GPU : UnmanagedWrapper<IADLXGPU>
    {
        public GPU(IADLXGPU gpu) : base(gpu)
        {
            var ptr = ADLX.new_stringP_Ptr().DisposeWith(ADLX.delete_stringP_Ptr, Disposable);
            UnmanagedInterface.Name(ptr).ThrowIfError("Couldn't get GPU name");
            Name = ADLX.stringP_Ptr_value(ptr);
        }

        public string Name { get; }
    }
}
