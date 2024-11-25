using DynamicData;
using DynamicData.PLinq;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace SilvaViridis.Components
{
    public abstract class ViewModelBase : ReactiveObject
    {
        protected static IDisposable InitCache<TObject, TKey>(
            out SourceCache<TObject, TKey> cache,
            out ReadOnlyObservableCollection<TObject> data,
            Func<TObject, TKey> keySelector,
            Func<TObject, bool>? filter = null,
            Func<TObject, IComparable>? sortBy = null
        )
            where TObject : notnull
            where TKey : notnull
        {
            cache = new(keySelector);

            var query = cache.Connect();

            if (filter is not null)
            {
                query = query.Filter(filter);
            }

            if (sortBy is not null)
            {
                query = query.SortBy(sortBy);
            }

            return query
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out data)
                .Subscribe();
        }
    }
}
