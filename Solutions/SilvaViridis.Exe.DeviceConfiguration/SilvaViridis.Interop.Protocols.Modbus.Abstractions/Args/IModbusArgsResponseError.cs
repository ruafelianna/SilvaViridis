using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Assets.Translations;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args
{
    public interface IModbusArgsResponseError : IModbusArgsResponse
    {
        ModbusExceptionCodes ExceptionCode { get; }

        ITranslationUnit Reason => _reasons[ExceptionCode];

        private static readonly Dictionary<ModbusExceptionCodes, ITranslationUnit> _reasons = new()
        {
            [ModbusExceptionCodes.Acknowledge] = ModbusExceptionCodesStrings.Acknowledge,
            [ModbusExceptionCodes.GatewayPathUnavailable] = ModbusExceptionCodesStrings.GatewayPathUnavailable,
            [ModbusExceptionCodes.GatewayTargetDeviceFailedToRespond] = ModbusExceptionCodesStrings.GatewayTargetDeviceFailedToRespond,
            [ModbusExceptionCodes.InvalidDataAddress] = ModbusExceptionCodesStrings.InvalidDataAddress,
            [ModbusExceptionCodes.InvalidDataValue] = ModbusExceptionCodesStrings.InvalidDataValue,
            [ModbusExceptionCodes.InvalidFunctionCode] = ModbusExceptionCodesStrings.InvalidFunctionCode,
            [ModbusExceptionCodes.MemoryParityError] = ModbusExceptionCodesStrings.MemoryParityError,
            [ModbusExceptionCodes.NegativeAcknowledge] = ModbusExceptionCodesStrings.NegativeAcknowledge,
            [ModbusExceptionCodes.ServerDeviceBusy] = ModbusExceptionCodesStrings.ServerDeviceBusy,
            [ModbusExceptionCodes.ServerDeviceFailure] = ModbusExceptionCodesStrings.ServerDeviceFailure,
        };
    }
}
