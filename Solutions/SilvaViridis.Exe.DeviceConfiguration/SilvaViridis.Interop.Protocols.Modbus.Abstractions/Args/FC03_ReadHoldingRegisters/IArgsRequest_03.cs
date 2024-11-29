using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Immutable;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC03_ReadHoldingRegisters
{
    public interface IArgsRequest_03 : IModbusArgsRequest
    {
        ushort StartingAddress { get; }

        ushort QuantityOfRegisters { get; }

        const ushort MinQuantity = 0x0001;

        const ushort MaxQuantity = 0x007D; // 125

        const ModbusDataTypes DataType
            = ModbusDataTypes.HoldingRegister;

        const ModbusFunctionCodes StandardCode
            = ModbusFunctionCodes.ReadHoldingRegisters;

        static ImmutableArray<ModbusExceptionCodes> ExpectedExceptionCodes
            => [
                ModbusExceptionCodes.InvalidFunctionCode,
                ModbusExceptionCodes.InvalidDataAddress,
                ModbusExceptionCodes.InvalidDataValue,
                ModbusExceptionCodes.ServerDeviceFailure,
            ];
    }
}
