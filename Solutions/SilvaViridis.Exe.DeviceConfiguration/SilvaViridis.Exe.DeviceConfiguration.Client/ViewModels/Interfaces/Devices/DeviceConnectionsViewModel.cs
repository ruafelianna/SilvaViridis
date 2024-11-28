using DynamicData;
using ReactiveUI;
using SilvaViridis.Components;
using SilvaViridis.Components.Generators;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public partial class DeviceConnectionsViewModel : ViewModelBase
    {
        public DeviceConnectionsViewModel()
        {
            Init(out _devPortsCache, out _devPorts);
        }

        [SourceCache(KeyTypeName = nameof(IComparable))]
        private readonly ReadOnlyObservableCollection<DevicePortViewModel> _devPorts;

        private static void Init(
            out SourceCache<DevicePortViewModel, IComparable> devPortsCache,
            out ReadOnlyObservableCollection<DevicePortViewModel> devPorts
        )
        {
            devPortsCache = new(devPort => devPort.ConnectionInfo.SortKey);

            devPortsCache
                .Connect()
                .SortBy(devPort => devPort.ConnectionInfo.SortKey)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out devPorts)
                .Subscribe();
        }
    }
}
