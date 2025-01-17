using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions;
using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections
{
    public partial class SerialPortConnectionViewModel :
        ViewModelBase,
        IConnectionViewModel
    {
        public SerialPortConnectionViewModel(string portName)
        {
            PortName = portName;
            SortKey = PortName;
        }

        public IComparable SortKey { get; }

        public string PortName { get; }
    }
}
