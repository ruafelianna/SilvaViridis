using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions
{
    public interface IProtocolViewModel
    {
        IComparable SortKey { get; }
    }
}
