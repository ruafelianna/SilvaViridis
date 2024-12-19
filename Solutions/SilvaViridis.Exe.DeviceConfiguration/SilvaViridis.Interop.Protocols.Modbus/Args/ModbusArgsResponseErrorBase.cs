using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Args
{
    public class ModbusArgsResponseErrorBase :
        ModbusArgsBase,
        IModbusArgsResponseError
    {
        #region Ctor

        public ModbusArgsResponseErrorBase(
            byte rawCode,
            byte exCode
        ) : base(rawCode)
            => InitExceptionCode(exCode, out _exceptionCode);

        public ModbusArgsResponseErrorBase(
            byte rawCode,
            ModbusExceptionCodes exCode
        ) : base(rawCode)
            => InitExceptionCode(exCode, out _exceptionCode);

        public ModbusArgsResponseErrorBase(
            ModbusFunctionCodes enumCode,
            byte exCode
        ) : base(enumCode)
            => InitExceptionCode(exCode, out _exceptionCode);

        public ModbusArgsResponseErrorBase(
            ModbusFunctionCodes enumCode,
            ModbusExceptionCodes exCode
        ) : base(enumCode)
            => InitExceptionCode(exCode, out _exceptionCode);

        public static IModbusArgsResponseError Create(
            byte rawCode,
            byte exCode
        ) => new ModbusArgsResponseErrorBase(rawCode, exCode);

        public static IModbusArgsResponseError Create(
            byte rawCode,
            ModbusExceptionCodes exCode
        ) => new ModbusArgsResponseErrorBase(rawCode, exCode);

        public static IModbusArgsResponseError Create(
            ModbusFunctionCodes enumCode,
            byte exCode
        ) => new ModbusArgsResponseErrorBase(enumCode, exCode);

        public static IModbusArgsResponseError Create(
            ModbusFunctionCodes enumCode,
            ModbusExceptionCodes exCode
        ) => new ModbusArgsResponseErrorBase(enumCode, exCode);

        #endregion

        #region Properties

        private readonly ModbusExceptionCodes _exceptionCode;
        public ModbusExceptionCodes ExceptionCode => _exceptionCode;

        public override IReadOnlyList<byte> RawData
            => [(byte)ExceptionCode];

        #endregion

        #region Init

        protected override void InitFunctionCode(
            byte rawCode,
            out ModbusFunctionCodes functionCode,
            out byte rawFunctionCode
        )
        {
            var code = unchecked((byte)(rawCode & 0x7F));

            if (!Enum.IsDefined(typeof(ModbusFunctionCodes), code))
            {
                throw new NotImplementedException();
            }

            functionCode = (ModbusFunctionCodes)code;
            rawFunctionCode = rawCode;
        }

        protected override void InitFunctionCode(
            ModbusFunctionCodes enumCode,
            out ModbusFunctionCodes functionCode,
            out byte rawFunctionCode
        )
        {
            functionCode = enumCode;
            rawFunctionCode = unchecked(
                (byte)unchecked(
                    (byte)enumCode | 0x80
                )
            );
        }

        protected virtual void InitExceptionCode(
            byte rawCode,
            out ModbusExceptionCodes exceptionCode
        )
        {
            if (!Enum.IsDefined(typeof(ModbusExceptionCodes), rawCode))
            {
                throw new NotImplementedException();
            }

            exceptionCode = (ModbusExceptionCodes)rawCode;
        }

        protected virtual void InitExceptionCode(
            ModbusExceptionCodes enumCode,
            out ModbusExceptionCodes exceptionCode
        ) => exceptionCode = enumCode;

        #endregion
    }
}
