using SilvaViridis.Components;
using SilvaViridis.Components.Generators;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models;
using System.Collections.ObjectModel;
using System;
using DynamicData;
using System.Reactive.Linq;
using ReactiveUI;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Enums;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces
{
    public partial class DeviceConnectionsViewModel : ViewModelBase
    {
        public DeviceConnectionsViewModel()
        {
            Init(out _devPortsCache, out _devPorts);

            for (int i = 0; i < 10; i++)
            {
                var dev = new DevicePortViewModel(
                    $"Name {i}",
                    new($"Device {i}"),
                    AvailableConnections.SerialPort,
                    new SerialPortViewModel($"COM{i}")
                );

                _devPortsCache.AddOrUpdate(dev);
            }
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
