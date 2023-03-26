using ADLXWrapper.Bindings;
using System;

namespace ADLXWrapper
{
    public abstract class Wrapper<T> : IDisposable where T : IDisposable
    {
        protected readonly CompositeDisposable Disposable = new CompositeDisposable();
        internal T NativeInterface;

        protected Wrapper(T nativeInterface, ActionDisposable disposeUnmanagedRessources = null)
        {
            NativeInterface = nativeInterface.DisposeWith(Disposable);
            if (disposeUnmanagedRessources != null)
            {
                Disposable.Add(disposeUnmanagedRessources);
            }
        }

        public virtual void Dispose()
        {
            Disposable.Dispose();
        }
    }
}
