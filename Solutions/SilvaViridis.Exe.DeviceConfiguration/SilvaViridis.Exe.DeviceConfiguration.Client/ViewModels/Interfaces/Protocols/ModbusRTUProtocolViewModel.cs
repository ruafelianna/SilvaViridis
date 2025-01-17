using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions;
using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols
{
    public class ModbusRTUProtocolViewModel :
        ViewModelBase,
        IProtocolViewModel
    {
        public ModbusRTUProtocolViewModel(byte address)
        {
            Address = address;
            SortKey = Address;
        }

        public IComparable SortKey { get; }

        public byte Address { get; }
    }
}
