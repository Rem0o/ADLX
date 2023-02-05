using System;

namespace ADLXWrapper
{
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
}
