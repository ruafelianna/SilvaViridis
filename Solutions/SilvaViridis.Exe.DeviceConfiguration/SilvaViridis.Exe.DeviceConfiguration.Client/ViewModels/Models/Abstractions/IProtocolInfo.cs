using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Abstractions
{
    public interface IProtocolInfo
    {
        IComparable SortKey { get; }
    }
}
