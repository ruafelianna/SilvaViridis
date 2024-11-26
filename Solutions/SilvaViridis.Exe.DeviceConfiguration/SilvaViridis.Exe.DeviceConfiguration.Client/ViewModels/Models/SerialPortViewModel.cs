using DynamicData;
using ReactiveUI;
using SilvaViridis.Components;
using SilvaViridis.Components.Generators;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Enums;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models
{
    public partial class SerialPortViewModel : ViewModelBase, IConnectionInfo
    {
        public SerialPortViewModel(string portName)
        {
            PortName = portName;
            SortKey = PortName;

            Init(out _addressesCache, out _addresses);

            for (int i = 0; i < 10; i++)
            {
                var dev = new DeviceAddressViewModel(
                    null,
                    AvailableProtocols.ModbusRTU,
                    new ModbusRTUViewModel((byte)i)
                );

                _addressesCache.AddOrUpdate(dev);
            }
        }

        public IComparable SortKey { get; }

        public string PortName { get; }

        [SourceCache(KeyTypeName = nameof(IComparable))]
        private readonly ReadOnlyObservableCollection<DeviceAddressViewModel> _addresses;

        private static void Init(
            out SourceCache<DeviceAddressViewModel, IComparable> addressesCache,
            out ReadOnlyObservableCollection<DeviceAddressViewModel> addresses
        )
        {
            addressesCache = new(devAddr => devAddr.ProtocolInfo.SortKey);

            addressesCache
                .Connect()
                .SortBy(devAddr => devAddr.ProtocolInfo.SortKey)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out addresses)
                .Subscribe();
        }
    }
}
