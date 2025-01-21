using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Abstractions
{
    public interface IProtocolViewModel
    {
        IComparable SortKey { get; }
    }
}
