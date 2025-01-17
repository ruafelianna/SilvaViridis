using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Extensions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.DTOs;
using SilvaViridis.Interop.Ports.SerialPort.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections
{
    public partial class AddSerialPortConnectionViewModel<TSerialPortContext> :
        ValidatableViewModelBase,
        IAddConnectionViewModel,
        IAddSerialPortConnectionViewModel
        where TSerialPortContext : ISerialPortContext
    {
        public AddSerialPortConnectionViewModel()
        {
            var portsCache = new SourceCache<SerialPortOption, string>(
                x => x.Name
            );

            portsCache
                .Connect()
                .SortBy(x => x.Name)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out var ports)
                .Subscribe();

            AvailablePorts = ports;

            async Task refreshPorts()
            {
                var result = await TSerialPortContext.Ports;
                using var suspend = portsCache.SuspendNotifications();
                portsCache.Clear();
                portsCache.AddOrUpdate(
                    result.Select(name => new SerialPortOption(name))
                );
            }

            RefreshPorts = ReactiveCommand.CreateFromTask(refreshPorts);

            this.RuleNotNull(vm => vm.SelectedPort);
        }

        public IEnumerable<SerialPortOption> AvailablePorts { get; }

        [Reactive]
        private SerialPortOption? _selectedPort = null!;

        public ReactiveCommand<Unit, Unit> RefreshPorts { get; }

        public IAddConnectionDTO AsDTO()
            => ValidationContext.IsValid
                ? new AddSerialPortConnectionDTO(SelectedPort!.Name)
                : throw new InvalidOperationException();
    }
}
