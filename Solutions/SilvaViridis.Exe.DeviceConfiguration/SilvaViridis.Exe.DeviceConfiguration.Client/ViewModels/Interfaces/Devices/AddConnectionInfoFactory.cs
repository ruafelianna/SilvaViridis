using SilvaViridis.Common.Numerics;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;
using SilvaViridis.Interop.Ports.SerialPort.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public class AddConnectionInfoFactory<TSerialPortContext> :
        IAddConnectionInfoFactory
        where TSerialPortContext : ISerialPortContext
    {
        public AddConnectionInfoFactory(NumberRegex numberRegex)
        {
            _numberRegex = numberRegex;
        }

        public IAddConnectionInfo CreateSerialPortInfo()
            => new AddSerialPortViewModel<TSerialPortContext>(_numberRegex);

        private readonly NumberRegex _numberRegex;
    }
}
