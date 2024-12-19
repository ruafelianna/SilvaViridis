using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Args
{
    public abstract class ModbusArgsBase : IModbusArgs
    {
        #region Ctor

        public ModbusArgsBase(byte rawCode)
            => InitFunctionCode(
                rawCode,
                out _functionCode,
                out _rawFunctionCode
            );

        public ModbusArgsBase(ModbusFunctionCodes enumCode)
            => InitFunctionCode(
                enumCode,
                out _functionCode,
                out _rawFunctionCode
            );

        #endregion

        #region Properties

        private readonly byte _rawFunctionCode;
        public byte RawFunctionCode => _rawFunctionCode;

        private readonly ModbusFunctionCodes _functionCode;
        public ModbusFunctionCodes FunctionCode => _functionCode;

        public abstract IReadOnlyList<byte> RawData { get; }

        #endregion

        #region Init

        protected virtual void InitFunctionCode(
            byte rawCode,
            out ModbusFunctionCodes functionCode,
            out byte rawFunctionCode
        )
        {
            if (!Enum.IsDefined(typeof(ModbusFunctionCodes), rawCode))
            {
                throw new NotImplementedException();
            }

            functionCode = (ModbusFunctionCodes)rawCode;
            rawFunctionCode = rawCode;
        }

        protected virtual void InitFunctionCode(
            ModbusFunctionCodes enumCode,
            out ModbusFunctionCodes functionCode,
            out byte rawFunctionCode
        )
        {
            functionCode = enumCode;
            rawFunctionCode = (byte)enumCode;
        }

        #endregion
    }
}
