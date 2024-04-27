using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public abstract class ADLXInterfaceWrapper<T> : Wrapper<T> where T : IADLXInterface
    {
        protected ADLXInterfaceWrapper(T nativeInterface) : base(nativeInterface)
        {
        }

        public override void Dispose()
        {
            NativeInterface.Release();
            base.Dispose();
        }
    }
}
