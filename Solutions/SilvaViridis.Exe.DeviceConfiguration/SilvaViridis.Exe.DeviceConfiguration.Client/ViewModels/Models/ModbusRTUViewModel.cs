using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Abstractions;
using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models
{
    public class ModbusRTUViewModel : ViewModelBase, IProtocolInfo
    {
        public ModbusRTUViewModel(byte address)
        {
            Address = address;
            SortKey = Address;
        }

        public IComparable SortKey { get; }

        public byte Address { get; }
    }
}
