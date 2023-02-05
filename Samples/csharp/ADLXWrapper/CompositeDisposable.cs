using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        public CompositeDisposable() { }

        public void Add(IDisposable item)
        {
            _disposables.Add(item);
        }

        public void Dispose()
        {
            _disposables.ForEach(d => d.Dispose());
            _disposables.Clear();
        }
    }
}
