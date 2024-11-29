using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args
{
    public interface IModbusArgs
    {
        byte RawFunctionCode { get; }

        ModbusFunctionCodes FunctionCode { get; }

        IReadOnlyList<byte> RawData { get; }
    }
}
