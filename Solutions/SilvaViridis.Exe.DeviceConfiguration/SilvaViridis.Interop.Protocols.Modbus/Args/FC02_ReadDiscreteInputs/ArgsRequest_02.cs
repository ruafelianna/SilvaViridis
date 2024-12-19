using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC02_ReadDiscreteInputs
{
    public class ArgsRequest_02 :
        ModbusArgsRequestReadBase,
        IArgsRequest_02
    {
        public ArgsRequest_02(
            ushort startAddress,
            ushort cntInputs
        ) : this(
            IArgsRequest_02.StandardCode,
            startAddress,
            cntInputs
        )
        {
        }

        public ArgsRequest_02(
            byte rawCode,
            ushort startAddress,
            ushort cntInputs
        ) : base(
            rawCode,
            startAddress,
            cntInputs
        )
        {
        }

        public ArgsRequest_02(
            ModbusFunctionCodes enumCode,
            ushort startAddress,
            ushort cntInputs
        ) : base(
            enumCode,
            startAddress,
            cntInputs
        )
        {
        }

        public ushort QuantityOfInputs
            => QuantityOfItems;

        protected override ushort MinQuantity
            => IArgsRequest_02.MinQuantity;

        protected override ushort MaxQuantity
            => IArgsRequest_02.MaxQuantity;
    }
}
