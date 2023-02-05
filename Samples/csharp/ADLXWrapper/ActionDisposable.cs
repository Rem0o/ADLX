using System;

namespace ADLXWrapper
{
    public class ActionDisposable : IDisposable
    {
        private Action _action;

        public ActionDisposable(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action?.Invoke();
            _action = null;
        }

        public static implicit operator ActionDisposable(Action action)
        {
            return new ActionDisposable (action);
        }
    }
}
