using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions
{
    public interface IAddSerialPortViewModel : IReactiveObject
    {
        IEnumerable<ISerialPortName> AvailablePorts { get; }

        ISerialPortName? SelectedPort { get; set; }

        bool AddAddresses { get; set; }

        string? AddressesQuantity_Str { get; set; }

        string? AddressesStartingWith_Str { get; set; }

        ReactiveCommand<Unit, Unit> RefreshPorts { get; }
    }
}
