using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLXWrapper.TestConsole
{
    internal static class Extensions
    {
        public static T DisposeWith<T>(this T item, CompositeDisposable compositeDisposable) where T : IDisposable
        {
            compositeDisposable.Add(item);
            return item;
        }

        public static T DisposeWith<T>(this T item, Action<T> _disposeAction, CompositeDisposable compositeDisposable)
        {
            compositeDisposable.Add(new ActionDisposable(() => _disposeAction(item)));
            return item;
        }
    }
}
