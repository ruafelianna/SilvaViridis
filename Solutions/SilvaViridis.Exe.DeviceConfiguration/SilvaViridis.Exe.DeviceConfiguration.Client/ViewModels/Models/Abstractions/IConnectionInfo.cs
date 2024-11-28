using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Abstractions
{
    public interface IConnectionInfo
    {
        IComparable SortKey { get; }
    }
}
