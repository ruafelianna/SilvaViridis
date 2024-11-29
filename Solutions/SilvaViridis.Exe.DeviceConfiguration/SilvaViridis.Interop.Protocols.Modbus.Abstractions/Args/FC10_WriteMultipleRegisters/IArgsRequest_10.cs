using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC10_WriteMultipleRegisters
{
    public interface IArgsRequest_10 : IModbusArgsRequest
    {
        ushort StartingAddress { get; }

        ushort QuantityOfRegisters { get; }

        byte ByteCount { get; }

        IReadOnlyList<byte> RegistersValue { get; }

        const ushort MinQuantity = 0x0001;

        const ushort MaxQuantity = 0x007B; // 123

        const ModbusDataTypes DataType
            = ModbusDataTypes.HoldingRegister;

        const ModbusFunctionCodes StandardCode
            = ModbusFunctionCodes.WriteMultipleRegisters;

        static ImmutableArray<ModbusExceptionCodes> ExpectedExceptionCodes
            => [
                ModbusExceptionCodes.InvalidFunctionCode,
                ModbusExceptionCodes.InvalidDataAddress,
                ModbusExceptionCodes.InvalidDataValue,
                ModbusExceptionCodes.ServerDeviceFailure,
            ];
    }
}
