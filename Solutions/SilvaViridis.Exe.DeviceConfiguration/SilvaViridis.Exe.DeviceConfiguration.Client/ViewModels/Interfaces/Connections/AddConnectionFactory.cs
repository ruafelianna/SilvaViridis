using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions;
using SilvaViridis.Interop.Ports.SerialPort.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections
{
    public class AddConnectionFactory<TSerialPortContext> :
        IAddConnectionFactory
        where TSerialPortContext : ISerialPortContext
    {
        public IAddConnectionViewModel CreateSerialPortConnection()
            => new AddSerialPortConnectionViewModel<TSerialPortContext>();
    }
}
