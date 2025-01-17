using ReactiveUI;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.DTOs;
using System.Collections.Generic;
using System.Reactive;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions
{
    public interface IAddSerialPortConnectionViewModel : IReactiveObject
    {
        IEnumerable<SerialPortOption> AvailablePorts { get; }

        SerialPortOption? SelectedPort { get; set; }

        ReactiveCommand<Unit, Unit> RefreshPorts { get; }
    }
}
