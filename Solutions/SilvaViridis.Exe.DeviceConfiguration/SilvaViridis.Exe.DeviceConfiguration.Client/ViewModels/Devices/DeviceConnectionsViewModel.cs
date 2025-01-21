using DynamicData;
using ReactiveUI;
using SilvaViridis.Components;
using SilvaViridis.Components.Generators;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices
{
    public partial class DeviceConnectionsViewModel : ViewModelBase
    {
        public DeviceConnectionsViewModel(
            Action<ViewModelBase> showContent,
            Action hideContent,
            IEnumerable<DeviceViewModel> devices,
            Func<Task> refreshDevices,
            IAddConnectionFactory addConnectionFactory,
            IAddProtocolFactory addProtocolFactory
        )
        {
            _connectionsCache = new(devPort => devPort.ConnectionInfo.SortKey);

            _connectionsCache
                .Connect()
                .SortBy(devPort => devPort.ConnectionInfo.SortKey)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _connections)
                .Subscribe();

            AddConnection = ReactiveCommand.CreateRunInBackground(
                () => {
                    AddDeviceConnectionViewModel addConn = null!;

                    addConn = new AddDeviceConnectionViewModel(
                        async () =>
                        {
                            var dto = addConn.AsDTO();

                            _connectionsCache.AddOrUpdate(
                                new DeviceConnectionViewModel(dto)
                            );

                            hideContent();
                        },
                        async () => hideContent(),
                        devices,
                        refreshDevices,
                        addConnectionFactory,
                        addProtocolFactory
                    );

                    showContent(addConn);
                }
            );
        }

        [SourceCache(KeyTypeName = nameof(IComparable))]
        private readonly ReadOnlyObservableCollection<DeviceConnectionViewModel> _connections;

        public ReactiveCommand<Unit, Unit> AddConnection { get; }
    }
}
