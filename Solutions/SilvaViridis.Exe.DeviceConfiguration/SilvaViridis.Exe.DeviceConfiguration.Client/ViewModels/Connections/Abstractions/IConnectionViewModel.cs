using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions
{
    public interface IConnectionViewModel
    {
        IComparable SortKey { get; }
    }
}
