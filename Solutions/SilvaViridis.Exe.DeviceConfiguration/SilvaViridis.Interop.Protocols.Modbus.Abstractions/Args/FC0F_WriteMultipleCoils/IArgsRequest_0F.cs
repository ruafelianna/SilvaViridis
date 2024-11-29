using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC0F_WriteMultipleCoils
{
    public interface IArgsRequest_0F : IModbusArgsRequest
    {
        ushort StartingAddress { get; }

        ushort QuantityOfOutputs { get; }

        byte ByteCount { get; }

        IReadOnlyList<byte> OutputsValue { get; }

        const ushort MinQuantity = 0x0001;

        const ushort MaxQuantity = 0x07B0; // 1968

        const ModbusDataTypes DataType
            = ModbusDataTypes.DiscreteOutput;

        const ModbusFunctionCodes StandardCode
            = ModbusFunctionCodes.WriteMultipleCoils;

        static ImmutableArray<ModbusExceptionCodes> ExpectedExceptionCodes
            => [
                ModbusExceptionCodes.InvalidFunctionCode,
                ModbusExceptionCodes.InvalidDataAddress,
                ModbusExceptionCodes.InvalidDataValue,
                ModbusExceptionCodes.ServerDeviceFailure,
            ];
    }
}
