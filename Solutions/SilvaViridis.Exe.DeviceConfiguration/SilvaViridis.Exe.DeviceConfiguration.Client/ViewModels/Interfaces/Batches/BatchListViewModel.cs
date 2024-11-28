using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Text.Extensions;
using SilvaViridis.Components;
using SilvaViridis.Components.Generators;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Batches
{
    public partial class BatchListViewModel : ViewModelBase
    {
        public BatchListViewModel()
            => Init(out _batchesCache, out _batches);

        [SourceCache(KeyTypeName = nameof(Int64))]
        private readonly ReadOnlyObservableCollection<BatchViewModel> _batches;

        [Reactive]
        private string? _searchName;

        private void Init(
            out SourceCache<BatchViewModel, long> batchesCache,
            out ReadOnlyObservableCollection<BatchViewModel> batches
        )
        {
            static Func<BatchViewModel, bool> searchNameFilter(
                string? searchName
            ) => batch =>
                searchName.IsNullOrEmpty()
                || batch.Name.Contains(
                    searchName.Trim(),
                    StringComparison.OrdinalIgnoreCase
                );

            var searchNamePredicate = this
                .WhenAnyValue(vm => vm.SearchName)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .DistinctUntilChanged()
                .Select(searchNameFilter);

            batchesCache = new(batch => batch.Id);

            batchesCache
                .Connect()
                .Filter(searchNamePredicate)
                .SortBy(
                    batch => batch.Created,
                    SortDirection.Descending
                )
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out batches)
                .Subscribe();
        }
    }
}
