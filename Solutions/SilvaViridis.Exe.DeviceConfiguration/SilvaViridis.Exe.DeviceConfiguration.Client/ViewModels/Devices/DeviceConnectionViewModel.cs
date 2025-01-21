using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Generators;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Enums;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices
{
    public partial class DeviceConnectionViewModel : ViewModelBase
    {
        public DeviceConnectionViewModel(
            string name,
            DeviceViewModel device,
            AvailableConnections connection,
            AvailableProtocols protocol,
            IConnectionViewModel connectionInfo,
            IEnumerable<DeviceAddressViewModel>? addresses = null
        ) : this(
            name,
            device,
            ConnectionOption.Create(connection),
            ProtocolOption.Create(protocol),
            connectionInfo,
            addresses
        )
        {
        }

        public DeviceConnectionViewModel(AddDeviceConnectionDTO dto) :
            this(
                dto.Name,
                dto.Device,
                dto.Connection,
                dto.Protocol,
                dto.ConnectionInfo switch {
                    IAddSerialPortConnectionDTO con_sp
                        => new SerialPortConnectionViewModel(con_sp.PortName),
                    _ => throw new NotSupportedException()
                }
            )
        {
            _addressesCache.AddOrUpdate(
                dto.ProtocolInfo switch
                {
                    AddModbusRTUProtocolDTO pr_mbr
                        => Enumerable
                            .Range(0, pr_mbr.AddressesQuantity)
                            .Select(
                                x => new DeviceAddressViewModel(
                                    this,
                                    new ModbusRTUProtocolViewModel(
                                        (byte)(x * pr_mbr.AddressesStep + pr_mbr.AddressesStartingWith)
                                    )
                                )
                            ),
                    _ => []
                }
            );
        }

        public DeviceConnectionViewModel(
            string name,
            DeviceViewModel device,
            ConnectionOption connection,
            ProtocolOption protocol,
            IConnectionViewModel connectionInfo,
            IEnumerable<DeviceAddressViewModel>? addresses = null
        )
        {
            Name = name;
            Device = device;
            Connection = connection;
            Protocol = protocol;
            ConnectionInfo = connectionInfo;

            _addressesCache = new(devAddr => devAddr.ProtocolInfo.SortKey);

            _addressesCache
                .Connect()
                .SortBy(devAddr => devAddr.ProtocolInfo.SortKey)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _addresses)
                .Subscribe();

            if (addresses is not null)
            {
                _addressesCache.AddOrUpdate(addresses);
            }
        }

        public string Name { get; }

        public DeviceViewModel Device { get; }

        public ConnectionOption Connection { get; }

        public ProtocolOption Protocol { get; }

        public IConnectionViewModel ConnectionInfo { get; }

        [Reactive]
        private bool _isChosen;

        [Reactive]
        private bool _isOnline;

        [SourceCache(KeyTypeName = nameof(IComparable))]
        private readonly ReadOnlyObservableCollection<DeviceAddressViewModel> _addresses;
    }
}
