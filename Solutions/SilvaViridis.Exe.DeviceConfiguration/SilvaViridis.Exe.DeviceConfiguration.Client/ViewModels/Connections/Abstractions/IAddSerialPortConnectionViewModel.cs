using ReactiveUI;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.DTOs;
using System.Collections.Generic;
using System.Reactive;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions
{
    public interface IAddSerialPortConnectionViewModel : IReactiveObject
    {
        IEnumerable<SerialPortOption> AvailablePorts { get; }

        SerialPortOption? SelectedPort { get; set; }

        ReactiveCommand<Unit, Unit> RefreshPorts { get; }
    }
}
