using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Extensions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Enums;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices
{
    public partial class AddDeviceConnectionViewModel : SavableViewModel
    {
        public AddDeviceConnectionViewModel(
            Func<Task> saveCallback,
            Func<Task> cancelCallback,
            IEnumerable<DeviceViewModel> devices,
            Func<Task> refreshDevices,
            IAddConnectionFactory connectionInfoFactory,
            IAddProtocolFactory protocolInfoFactory
        ) : base(saveCallback, cancelCallback)
        {
            Devices = devices;

            RefreshDevices = ReactiveCommand.CreateFromTask(refreshDevices);

            Connections = [
                ConnectionOption.SerialPort,
            ];

            static Func<ProtocolOption, bool> protocolFilter(
                ConnectionOption? connection
            ) => protocol =>
                connection is not null
                && connection.SupportedProtocols.Contains(protocol.Protocol);

            var protocolPredicate = this
                .WhenAnyValue(vm => vm.SelectedConnection)
                .Select(protocolFilter);

            var protocolsCache = new SourceCache<ProtocolOption, AvailableProtocols>(
                x => x.Protocol
            );

            protocolsCache.AddOrUpdate(ProtocolOption.ModbusRTU);

            protocolsCache
                .Connect()
                .Filter(protocolPredicate)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out var protocols)
                .Subscribe();

            Protocols = protocols;

            _canSelectProtocolHelper = this
                .WhenAnyValue(vm => vm.SelectedConnection)
                .Select(connection => connection is not null)
                .ToProperty(this, vm => vm.CanSelectProtocol);

            _connectionInfoHelper = this
                .WhenAnyValue( vm => vm.SelectedConnection)
                .Select(connection =>
                    connection is null
                        ? null
                        : connection.Connection switch
                        {
                            AvailableConnections.SerialPort
                                => connectionInfoFactory.CreateSerialPortConnection(),
                            _ => null,
                        }
                )
                .ToProperty(this, vm => vm.ConnectionInfo);

            _protocolInfoHelper = this
                .WhenAnyValue(vm => vm.SelectedProtocol)
                .Select(protocol =>
                    protocol is null
                        ? null
                        : protocol.Protocol switch
                        {
                            AvailableProtocols.ModbusRTU
                                => protocolInfoFactory.CreateModbusProtocol(),
                            _ => null,
                        }
                )
                .ToProperty(this, vm => vm.ProtocolInfo);

            this.RuleNotNullOrWhiteSpace(vm => vm.Name);

            this.RuleNotNull(vm => vm.SelectedDevice);

            this.RuleNotNull(vm => vm.SelectedConnection);

            this.RuleNotNull(vm => vm.SelectedProtocol);

            this.RuleNestedCtxValidIfExists(vm => vm.ConnectionInfo);

            this.RuleNestedCtxValidIfExists(vm => vm.ProtocolInfo);
        }

        public IEnumerable<DeviceViewModel> Devices { get; }

        public ReactiveCommand<Unit, Unit> RefreshDevices { get; }

        public IEnumerable<ConnectionOption> Connections { get; }

        public IEnumerable<ProtocolOption> Protocols { get; }

        [Reactive]
        private string? _name;

        [Reactive]
        private DeviceViewModel? _selectedDevice;

        [Reactive]
        private ConnectionOption? _selectedConnection;

        [Reactive]
        private ProtocolOption? _selectedProtocol;

        [ObservableAsProperty]
        private bool _canSelectProtocol;

        [ObservableAsProperty]
        private IAddConnectionViewModel? _connectionInfo;

        [ObservableAsProperty]
        private IAddProtocolViewModel? _protocolInfo;

        public AddDeviceConnectionDTO AsDTO()
            => ValidationContext.IsValid
                ? new(
                    Name!,
                    SelectedDevice!,
                    SelectedConnection!,
                    SelectedProtocol!,
                    ConnectionInfo!.AsDTO(),
                    ProtocolInfo!.AsDTO()
                )
                : throw new InvalidOperationException();
    }
}
