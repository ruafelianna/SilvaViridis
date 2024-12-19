using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC04_ReadInputRegisters
{
    public class ArgsRequest_04 :
        ModbusArgsRequestReadBase,
        IArgsRequest_04
    {
        public ArgsRequest_04(
            ushort startAddress,
            ushort cntRegs
        ) : this(
            IArgsRequest_04.StandardCode,
            startAddress,
            cntRegs
        )
        {
        }

        public ArgsRequest_04(
            byte rawCode,
            ushort startAddress,
            ushort cntRegs
        ) : base(
            rawCode,
            startAddress,
            cntRegs
        )
        {
        }

        public ArgsRequest_04(
            ModbusFunctionCodes enumCode,
            ushort startAddress,
            ushort cntRegs
        ) : base(
            enumCode,
            startAddress,
            cntRegs
        )
        {
        }

        public ushort QuantityOfInputRegisters
            => QuantityOfItems;

        protected override ushort MinQuantity
            => IArgsRequest_04.MinQuantity;

        protected override ushort MaxQuantity
            => IArgsRequest_04.MaxQuantity;
    }
}
