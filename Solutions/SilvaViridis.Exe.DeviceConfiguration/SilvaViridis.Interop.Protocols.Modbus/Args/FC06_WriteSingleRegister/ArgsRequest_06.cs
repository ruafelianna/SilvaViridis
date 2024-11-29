using SilvaViridis.Common.Numerics;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC06_WriteSingleRegister
{
    public class ArgsRequest_06 :
        ModbusArgsRequestWriteBase,
        IArgsRequest_06
    {
        public ArgsRequest_06(
            ushort regAddr,
            ushort value
        ) : this(
            IArgsRequest_06.StandardCode,
            regAddr,
            value
        )
        {
        }

        public ArgsRequest_06(
            byte rawCode,
            ushort regAddr,
            ushort value
        ) : base(
            rawCode,
            regAddr
        ) => InitRegisterValue(
            value,
            out _registerValue
        );

        public ArgsRequest_06(
            ModbusFunctionCodes enumCode,
            ushort regAddr,
            ushort value
        ) : base(
            enumCode,
            regAddr
        ) => InitRegisterValue(
            value,
            out _registerValue
        );

        public override IReadOnlyList<byte> RawData
            => [
                RegisterAddress.MSB(),
                RegisterAddress.LSB(),
                RegisterValue.MSB(),
                RegisterValue.LSB(),
            ];

        public ushort RegisterAddress => RegAddress;

        private readonly ushort _registerValue;
        public ushort RegisterValue => _registerValue;

        protected virtual void InitRegisterValue(
            ushort value,
            out ushort registerValue
        ) => registerValue = value;
    }
}
