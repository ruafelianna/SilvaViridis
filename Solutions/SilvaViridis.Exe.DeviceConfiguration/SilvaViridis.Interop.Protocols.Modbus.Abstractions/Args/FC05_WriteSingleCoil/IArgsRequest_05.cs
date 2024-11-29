using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Immutable;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC05_WriteSingleCoil
{
    public interface IArgsRequest_05 : IModbusArgsRequest
    {
        ushort OutputAddress { get; }

        ushort OutputValue { get; }

        static readonly ImmutableArray<ushort> AvailableValues = [
            Consts.Off,
            Consts.On
        ];

        const ModbusDataTypes DataType
            = ModbusDataTypes.DiscreteOutput;

        const ModbusFunctionCodes StandardCode
            = ModbusFunctionCodes.WriteSingleCoil;

        static ImmutableArray<ModbusExceptionCodes> ExpectedExceptionCodes
            => [
                ModbusExceptionCodes.InvalidFunctionCode,
                ModbusExceptionCodes.InvalidDataAddress,
                ModbusExceptionCodes.InvalidDataValue,
                ModbusExceptionCodes.ServerDeviceFailure,
            ];
    }
}
