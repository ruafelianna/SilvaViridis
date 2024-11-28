using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;
using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public partial class SerialPortViewModel :
        ViewModelBase,
        IConnectionInfo
    {
        public SerialPortViewModel(string portName)
        {
            PortName = portName;
            SortKey = PortName;
        }

        public IComparable SortKey { get; }

        public string PortName { get; }
    }
}
