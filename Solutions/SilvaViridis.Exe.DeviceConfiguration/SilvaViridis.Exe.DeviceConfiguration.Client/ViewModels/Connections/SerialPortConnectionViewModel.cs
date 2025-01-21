using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;
using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections
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
