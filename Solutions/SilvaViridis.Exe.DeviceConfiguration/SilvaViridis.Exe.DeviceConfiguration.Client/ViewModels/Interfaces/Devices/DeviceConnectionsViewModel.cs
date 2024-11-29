using DynamicData;
using ReactiveUI;
using SilvaViridis.Components;
using SilvaViridis.Components.Generators;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Enums;
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

            for (int i = 0; i < 10; i++)
            {
                _devPortsCache.AddOrUpdate(new DevicePortViewModel(
                    $"Name {i + 1}",
                    new($"Device {i + 1}"),
                    AvailableConnections.SerialPort,
                    AvailableProtocols.ModbusRTU,
                    new SerialPortViewModel($"COM{i + 1}")
                ));
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
