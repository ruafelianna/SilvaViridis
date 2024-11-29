using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Immutable;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC01_ReadCoils
{
    public interface IArgsRequest_01 : IModbusArgsRequest
    {
        ushort StartingAddress { get; }

        ushort QuantityOfCoils { get; }

        const ushort MinQuantity = 0x0001;

        const ushort MaxQuantity = 0x07D0; // 2000

        const ModbusDataTypes DataType
            = ModbusDataTypes.DiscreteOutput;

        const ModbusFunctionCodes StandardCode
            = ModbusFunctionCodes.ReadCoils;

        static ImmutableArray<ModbusExceptionCodes> ExpectedExceptionCodes
            => [
                ModbusExceptionCodes.InvalidFunctionCode,
                ModbusExceptionCodes.InvalidDataAddress,
                ModbusExceptionCodes.InvalidDataValue,
                ModbusExceptionCodes.ServerDeviceFailure,
            ];
    }
}
