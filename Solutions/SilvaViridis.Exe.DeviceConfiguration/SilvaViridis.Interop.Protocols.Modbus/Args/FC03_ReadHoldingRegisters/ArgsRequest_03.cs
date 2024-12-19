using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC03_ReadHoldingRegisters
{
    public class ArgsRequest_03 :
        ModbusArgsRequestReadBase,
        IArgsRequest_03
    {
        public ArgsRequest_03(
            ushort startAddress,
            ushort cntRegs
        ) : this(
            IArgsRequest_03.StandardCode,
            startAddress,
            cntRegs
        )
        {
        }

        public ArgsRequest_03(
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

        public ArgsRequest_03(
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

        public ushort QuantityOfRegisters
            => QuantityOfItems;

        protected override ushort MinQuantity
            => IArgsRequest_03.MinQuantity;

        protected override ushort MaxQuantity
            => IArgsRequest_03.MaxQuantity;
    }
}
