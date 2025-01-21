using SilvaViridis.Components;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices
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
