using ADLXWrapper.Bindings;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLXWrapper
{
    public static class Extensions
    {
        public static T DisposeWith<T>(this T item, CompositeDisposable compositeDisposable) where T : IDisposable
        {
            compositeDisposable.Add(item);
            return item;
        }

        public static T DisposeInterfaceWith<T>(this T item, CompositeDisposable compositeDisposable) where T: IADLXInterface
        {
            compositeDisposable.Add(item);
            compositeDisposable.Add(new ActionDisposable(() => item.Release()));
            return item;
        }

        private static T DisposeWith<T>(this T item, Action<T> _disposeAction, CompositeDisposable compositeDisposable)
        {
            compositeDisposable.Add(new ActionDisposable(() => _disposeAction(item)));
            return item;
        }

        public static void ThrowIfError(this ADLX_RESULT result, string message)
        {
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXEception($"Result: {result} {Environment.NewLine}{message}");
            }
        }
    }
}
