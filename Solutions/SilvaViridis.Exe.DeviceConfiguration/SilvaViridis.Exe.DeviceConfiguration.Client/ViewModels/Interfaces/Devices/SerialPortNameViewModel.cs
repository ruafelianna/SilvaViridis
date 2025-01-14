using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public class SerialPortNameViewModel : ISerialPortName
    {
        public SerialPortNameViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
