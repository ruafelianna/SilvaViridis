using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Immutable;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC06_WriteSingleRegister
{
    public interface IArgsRequest_06 : IModbusArgsRequest
    {
        ushort RegisterAddress { get; }

        ushort RegisterValue { get; }

        const ushort MinValue = 0x0000;

        const ushort MaxValue = 0xFFFF;

        const ModbusDataTypes DataType
            = ModbusDataTypes.HoldingRegister;

        const ModbusFunctionCodes StandardCode
            = ModbusFunctionCodes.WriteSingleRegister;

        static ImmutableArray<ModbusExceptionCodes> ExpectedExceptionCodes
            => [
                ModbusExceptionCodes.InvalidFunctionCode,
                ModbusExceptionCodes.InvalidDataAddress,
                ModbusExceptionCodes.InvalidDataValue,
                ModbusExceptionCodes.ServerDeviceFailure,
            ];
    }
}
