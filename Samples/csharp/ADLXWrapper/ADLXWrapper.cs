using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public class ADLXWrapper : Wrapper<ADLXHelper>
    {
        public ADLXWrapper() : base(new ADLXHelper())
        {
            NativeInterface.Initialize().ThrowIfError("Couldn't initialize ADLX");
        }

        public SystemServices GetSystemServices()
        {
            return new SystemServices(NativeInterface.GetSystemServices(), this);
        }

        public override void Dispose()
        {
            NativeInterface.Terminate().ThrowIfError("Couldn't terminate ADLX");
            base.Dispose();
        }
    }
}
