using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public class DeviceTypeViewModel : IDeviceType
    {
        public DeviceTypeViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
