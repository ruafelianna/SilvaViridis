using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Immutable;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC04_ReadInputRegisters
{
    public interface IArgsRequest_04 : IModbusArgsRequest
    {
        ushort StartingAddress { get; }

        ushort QuantityOfInputRegisters { get; }

        const ushort MinQuantity = 0x0001;

        const ushort MaxQuantity = 0x007D; // 125

        const ModbusDataTypes DataType
            = ModbusDataTypes.InputRegister;

        const ModbusFunctionCodes StandardCode
            = ModbusFunctionCodes.ReadInputRegisters;

        static ImmutableArray<ModbusExceptionCodes> ExpectedExceptionCodes
            => [
                ModbusExceptionCodes.InvalidFunctionCode,
                ModbusExceptionCodes.InvalidDataAddress,
                ModbusExceptionCodes.InvalidDataValue,
                ModbusExceptionCodes.ServerDeviceFailure,
            ];
    }
}
