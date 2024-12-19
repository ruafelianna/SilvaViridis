using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.Factory;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.Factory
{
    public class ModbusArgsErrorFactory : IModbusArgsErrorFactory
    {
        public DCreateError<IModbusArgsResponseError> Create
            => ModbusArgsResponseErrorBase.Create;
    }
}
