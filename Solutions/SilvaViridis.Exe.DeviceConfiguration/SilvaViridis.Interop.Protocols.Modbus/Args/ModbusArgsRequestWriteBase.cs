using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;

namespace SilvaViridis.Interop.Protocols.Modbus.Args
{
    public abstract class ModbusArgsRequestWriteBase : ModbusArgsBase
    {
        #region Ctor

        public ModbusArgsRequestWriteBase(
            byte rawCode,
            ushort regAddr
        ) : base(rawCode)
            => InitRegAddress(regAddr, out _regAddress);

        public ModbusArgsRequestWriteBase(
            ModbusFunctionCodes enumCode,
            ushort regAddr
        ) : base(enumCode)
            => InitRegAddress(regAddr, out _regAddress);

        #endregion

        #region Properties

        private readonly ushort _regAddress;
        protected ushort RegAddress => _regAddress;

        #endregion

        #region Init

        protected virtual void InitRegAddress(
            ushort address,
            out ushort regAddress
        ) => regAddress = address;

        #endregion
    }
}
