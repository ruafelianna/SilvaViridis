using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;
using SilvaViridis.Interop.Ports.SerialPort.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections
{
    public class AddConnectionFactory<TSerialPortContext> :
        IAddConnectionFactory
        where TSerialPortContext : ISerialPortContext
    {
        public IAddConnectionViewModel CreateSerialPortConnection()
            => new AddSerialPortConnectionViewModel<TSerialPortContext>();
    }
}
