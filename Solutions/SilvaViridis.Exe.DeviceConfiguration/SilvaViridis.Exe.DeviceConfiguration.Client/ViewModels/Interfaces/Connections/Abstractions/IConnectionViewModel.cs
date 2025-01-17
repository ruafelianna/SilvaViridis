using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions
{
    public interface IConnectionViewModel
    {
        IComparable SortKey { get; }
    }
}
