using SilvaViridis.Common.Numerics;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC10_WriteMultipleRegisters
{
    public class ArgsRequest_10 :
        ModbusArgsRequestWriteBase,
        IArgsRequest_10
    {
        public ArgsRequest_10(
            ushort startAddress,
            IReadOnlyList<byte> regsVal
        ) : this(
            IArgsRequest_10.StandardCode,
            startAddress,
            regsVal
        )
        {
        }

        public ArgsRequest_10(
            byte rawCode,
            ushort startAddress,
            IReadOnlyList<byte> regsVal
        ) : base(
            rawCode,
            startAddress
        ) => Init(
            regsVal,
            out _byteCount,
            out _quantityOfRegisters,
            out _registersValue
        );

        public ArgsRequest_10(
            ModbusFunctionCodes enumCode,
            ushort startAddress,
            IReadOnlyList<byte> regsVal
        ) : base(
            enumCode,
            startAddress
        ) => Init(
            regsVal,
            out _byteCount,
            out _quantityOfRegisters,
            out _registersValue
        );

        public override IReadOnlyList<byte> RawData
            => [
                StartingAddress.MSB(),
                StartingAddress.LSB(),
                QuantityOfRegisters.MSB(),
                QuantityOfRegisters.LSB(),
                ByteCount,
                .. RegistersValue,
            ];

        public ushort StartingAddress => RegAddress;

        private readonly ushort _quantityOfRegisters;
        public ushort QuantityOfRegisters => _quantityOfRegisters;

        private readonly byte _byteCount;
        public byte ByteCount => _byteCount;

        private readonly IReadOnlyList<byte> _registersValue;
        public IReadOnlyList<byte> RegistersValue => _registersValue;

        protected virtual void InitByteCount(
            IReadOnlyList<byte> regsVal,
            out byte byteCount
        )
        {
            var count = unchecked((byte)regsVal.Count);

            ArgumentOutOfRangeException.ThrowIfNotEqual(count.IsEven(), true);

            byteCount = count;
        }

        protected virtual void InitQuantityOfRegisters(
            out ushort quantityOfRegisters
        )
        {
            var quantity = ByteCount >> 1;

            ArgumentOutOfRangeException.ThrowIfLessThan(
                quantity, IArgsRequest_10.MinQuantity);

            ArgumentOutOfRangeException.ThrowIfGreaterThan(
                quantity, IArgsRequest_10.MaxQuantity);

            quantityOfRegisters = unchecked((ushort)quantity);
        }

        protected virtual void InitRegistersValue(
            IReadOnlyList<byte> regsValue,
            out IReadOnlyList<byte> registersValue
        ) => registersValue = regsValue;

        protected void Init(
            IReadOnlyList<byte> regsVal,
            out byte byteCount,
            out ushort quantityOfRegisters,
            out IReadOnlyList<byte> registersValue
        )
        {
            InitByteCount(regsVal, out byteCount);
            InitQuantityOfRegisters(out quantityOfRegisters);
            InitRegistersValue(regsVal, out registersValue);
        }
    }
}
