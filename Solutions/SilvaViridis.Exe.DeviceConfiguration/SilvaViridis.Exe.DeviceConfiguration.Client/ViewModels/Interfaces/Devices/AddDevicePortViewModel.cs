using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Components;
using SilvaViridis.Components.Extensions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Enums;
using System;
using System.Collections.Generic;
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

            Connections = new Dictionary<AvailableConnections, ITranslationUnit>
            {
                [AvailableConnections.SerialPort] = Strings.Conn_SerialPort,
            };

            Protocols = new Dictionary<AvailableProtocols, ITranslationUnit>
            {
                [AvailableProtocols.ModbusRTU] = Strings.Prot_ModbusRTU,
            };

            _canSelectProtocolHelper = this
                .WhenAnyValue(vm => vm.SelectedConnection)
                .Select(connection => connection is not null)
                .ToProperty(this, vm => vm.CanSelectProtocol);

            _connectionInfoHelper = this
                .WhenAnyValue(
                    vm => vm.SelectedConnection,
                    vm => vm.SelectedProtocol
                )
                .Where(selection =>
                    selection.Item1 is not null
                    && selection.Item2 is not null
                )
                .Select(selection => selection.Item2!.Value.Key switch {
                    AvailableProtocols.ModbusRTU
                        => connInfoFactory.CreateSerialPortInfo(),
                    _ => null,
                })
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

        public IReadOnlyDictionary<AvailableConnections, ITranslationUnit> Connections { get; }

        public IReadOnlyDictionary<AvailableProtocols, ITranslationUnit> Protocols { get; }

        [Reactive]
        private IDeviceType? _selectedDevice;

        [Reactive]
        private KeyValuePair<AvailableConnections, ITranslationUnit>? _selectedConnection;

        [Reactive]
        private KeyValuePair<AvailableProtocols, ITranslationUnit>? _selectedProtocol;

        [ObservableAsProperty]
        private bool _canSelectProtocol;

        [ObservableAsProperty]
        private IAddConnectionInfo? _connectionInfo;

        public ReactiveCommand<Unit, Unit> RefreshDevices { get; }
    }
}
