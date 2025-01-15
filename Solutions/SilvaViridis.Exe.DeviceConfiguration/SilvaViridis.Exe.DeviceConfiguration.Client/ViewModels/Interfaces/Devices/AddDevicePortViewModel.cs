using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Extensions;
using SilvaViridis.Components.Generators;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public partial class AddDevicePortViewModel : SavableViewModel
    {
        public AddDevicePortViewModel(
            Func<Task> saveCallback,
            Func<Task> cancelCallback,
            IEnumerable<IDeviceType> devices,
            Func<Task> refreshDevices,
            IAddConnectionInfoFactory connInfoFactory
        ) : base(saveCallback, cancelCallback)
        {
            Devices = devices;

            RefreshDevices = ReactiveCommand.CreateFromTask(refreshDevices);

            Connections = [
                new(
                    AvailableConnections.SerialPort,
                    Strings.Conn_SerialPort,
                    [
                        AvailableProtocols.ModbusRTU,
                    ]
                ),
            ];

            static Func<ProtocolOption, bool> protocolFilter(
                ConnectionOption? connection
            ) => protocol =>
                connection is not null
                && connection.SupportedProtocols.Contains(protocol.Protocol);

            var protocolPredicate = this
                .WhenAnyValue(vm => vm.SelectedConnection)
                .Select(protocolFilter);

            _protocolsCache = new(x => x.Protocol);

            _protocolsCache.AddOrUpdate(new ProtocolOption(
                AvailableProtocols.ModbusRTU,
                Strings.Prot_ModbusRTU
            ));

            _protocolsCache
                .Connect()
                .Filter(protocolPredicate)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _protocols)
                .Subscribe();

            _canSelectProtocolHelper = this
                .WhenAnyValue(vm => vm.SelectedConnection)
                .Select(connection => connection is not null)
                .ToProperty(this, vm => vm.CanSelectProtocol);

            _connectionInfoHelper = this
                .WhenAnyValue(
                    vm => vm.SelectedConnection,
                    vm => vm.SelectedProtocol
                )
                .Select(selection =>
                    selection.Item1 is null || selection.Item2 is null
                        ? null
                        : selection.Item2!.Protocol switch {
                            AvailableProtocols.ModbusRTU
                                => connInfoFactory.CreateSerialPortInfo(),
                            _ => null,
                        }
                )
                .ToProperty(this, vm => vm.ConnectionInfo);

            this.RuleNotNullOrWhiteSpace(vm => vm.Name);

            this.RuleNotNull(vm => vm.SelectedDevice);

            this.RuleNotNull(vm => vm.SelectedConnection);

            this.RuleNotNull(vm => vm.SelectedProtocol);

            this.RuleNestedCtxValidIfExists(vm => vm.ConnectionInfo);
        }

        [Reactive]
        private string? _name;

        public IEnumerable<IDeviceType> Devices { get; }

        public IEnumerable<ConnectionOption> Connections { get; }

        [SourceCache(
            KeyTypeName = nameof(AvailableProtocols),
            KeyTypeNamespace = $"{nameof(SilvaViridis)}.{nameof(Exe)}.{nameof(DeviceConfiguration)}.{nameof(Client)}.{nameof(ViewModels)}.{nameof(Interfaces)}.{nameof(Interfaces.Devices)}.{nameof(Enums)}"
        )]
        private readonly ReadOnlyObservableCollection<ProtocolOption> _protocols;

        [Reactive]
        private IDeviceType? _selectedDevice;

        [Reactive]
        private ConnectionOption? _selectedConnection;

        [Reactive]
        private ProtocolOption? _selectedProtocol;

        [ObservableAsProperty]
        private bool _canSelectProtocol;

        [ObservableAsProperty]
        private IAddConnectionInfo? _connectionInfo;

        public ReactiveCommand<Unit, Unit> RefreshDevices { get; }
    }
}
