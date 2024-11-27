using SilvaViridis.Components;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models
{
    public class DeviceViewModel : ViewModelBase
    {
        public DeviceViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
