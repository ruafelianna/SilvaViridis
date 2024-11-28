using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Generators;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Enums;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public partial class DevicePortViewModel : ViewModelBase
    {
        public DevicePortViewModel(
            string name,
            DeviceViewModel device,
            AvailableConnections connection,
            AvailableProtocols protocol,
            IConnectionInfo connectionInfo
        )
        {
            Name = name;
            Device = device;
            Connection = connection;
            Protocol = protocol;
            ConnectionInfo = connectionInfo;

            Init(out _addressesCache, out _addresses);
        }

        public string Name { get; }

        public DeviceViewModel Device { get; }

        public AvailableConnections Connection { get; }

        public AvailableProtocols Protocol { get; }

        public IConnectionInfo ConnectionInfo { get; }

        [Reactive]
        private bool _isChosen;

        [Reactive]
        private bool _isOnline;

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
