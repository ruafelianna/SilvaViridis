using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions
{
    public interface IConnectionInfo
    {
        IComparable SortKey { get; }
    }
}
