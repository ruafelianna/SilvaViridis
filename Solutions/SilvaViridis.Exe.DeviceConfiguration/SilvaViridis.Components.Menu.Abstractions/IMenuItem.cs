using System.ComponentModel;

namespace SilvaViridis.Components.Menu.Abstractions
{
    public interface IMenuItem : INotifyPropertyChanged
    {
        string Guid { get; }

        int SortKey { get; set; }

        bool IsVisible { get; set; }

        bool IsEnabled { get; set; }
    }
}
