using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Enums;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using SilvaViridis.Components.Extensions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public partial class AddDevicePortViewModel : SavableViewModel
    {
        public AddDevicePortViewModel(
            Func<Task> saveCallback,
            Func<Task> cancelCallback
        ) : base(saveCallback, cancelCallback)
        {
            Devices = ["TEST DEVICE"];

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
                .Select(selection => selection.Item2 switch {
                    AvailableProtocols.ModbusRTU
                        => new AddSerialPortViewModel(),
                    _ => null,
                })
                .ToProperty(this, vm => vm.ConnectionInfo);

            this.RuleNotNullOrWhiteSpace(vm => vm.Name);
        }

        [Reactive]
        private string? _name;

        public IEnumerable<string> Devices { get; }

        public IReadOnlyDictionary<AvailableConnections, ITranslationUnit> Connections { get; }

        public IReadOnlyDictionary<AvailableProtocols, ITranslationUnit> Protocols { get; }

        [Reactive]
        private string? _selectedDevice;

        [Reactive]
        private AvailableConnections? _selectedConnection;

        [Reactive]
        private AvailableProtocols? _selectedProtocol;

        [ObservableAsProperty]
        private bool _canSelectProtocol;

        [ObservableAsProperty]
        private IAddConnectionInfo? _connectionInfo;
    }
}
