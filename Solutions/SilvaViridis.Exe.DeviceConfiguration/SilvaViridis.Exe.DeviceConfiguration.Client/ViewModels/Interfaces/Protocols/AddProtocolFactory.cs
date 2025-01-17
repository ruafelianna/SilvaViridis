using SilvaViridis.Common.Numerics;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols
{
    public class AddProtocolFactory : IAddProtocolFactory
    {
        public AddProtocolFactory(NumberRegex numberRegex)
        {
            _numberRegex = numberRegex;
        }

        public IAddProtocolViewModel CreateModbusProtocol()
            => new AddModbusRTUProtocolViewModel(_numberRegex);

        private readonly NumberRegex _numberRegex;
    }
}
