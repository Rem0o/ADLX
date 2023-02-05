using ADLXWrapper.Bindings;

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
}
