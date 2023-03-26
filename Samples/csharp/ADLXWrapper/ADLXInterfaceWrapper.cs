using ADLXWrapper.Bindings;

namespace ADLXWrapper
{
    public abstract class ADLXInterfaceWrapper<T> : Wrapper<T> where T : IADLXInterface
    {
        protected ADLXInterfaceWrapper(T nativeInterface, ActionDisposable disposeUnmanagedRessources = null) : base(nativeInterface, disposeUnmanagedRessources)
        {

        }

        public override void Dispose()
        {
            NativeInterface.Release();
            base.Dispose();
        }
    }
}
