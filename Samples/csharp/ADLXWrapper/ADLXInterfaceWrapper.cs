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
            int referenceCount = NativeInterface.Release();
            if (referenceCount != 1)
            {
                string name = typeof(T).Name;
                throw new ADLXEception($"{name} still has {referenceCount} references.");
            }

            base.Dispose();
        }
    }
}
