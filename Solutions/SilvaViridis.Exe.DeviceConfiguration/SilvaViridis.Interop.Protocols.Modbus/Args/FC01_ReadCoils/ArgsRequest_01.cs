using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC01_ReadCoils
{
    public class ArgsRequest_01 :
        ModbusArgsRequestReadBase,
        IArgsRequest_01
    {
        public ArgsRequest_01(
            ushort startAddress,
            ushort cntCoils
        ) : this(
            IArgsRequest_01.StandardCode,
            startAddress,
            cntCoils
        )
        {
        }

        public ArgsRequest_01(
            byte rawCode,
            ushort startAddress,
            ushort cntCoils
        ) : base(
            rawCode,
            startAddress,
            cntCoils
        )
        {
        }

        public ArgsRequest_01(
            ModbusFunctionCodes enumCode,
            ushort startAddress,
            ushort cntCoils
        ) : base(
            enumCode,
            startAddress,
            cntCoils
        )
        {
        }

        public ushort QuantityOfCoils
            => QuantityOfItems;

        protected override ushort MinQuantity
            => IArgsRequest_01.MinQuantity;

        protected override ushort MaxQuantity
            => IArgsRequest_01.MaxQuantity;
    }
}
