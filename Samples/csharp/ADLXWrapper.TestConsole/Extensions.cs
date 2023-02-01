using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLXWrapper.TestConsole
{
    internal static class Extensions
    {
        public static T Using<T>(this T item, CompositeDisposable compositeDisposable ) where T : IDisposable
        {
            compositeDisposable.Add( item );
            return item;
        }
    }
}
