using SilvaViridis.Common.Numerics;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Args
{
    public abstract class ModbusArgsRequestReadBase : ModbusArgsBase
    {
        #region Ctor

        public ModbusArgsRequestReadBase(
            byte rawCode,
            ushort startAddr,
            ushort quantity
        ) : base(rawCode)
            => Init(
                startAddr,
                quantity,
                out _startingAddress,
                out _quantityOfItems
            );

        public ModbusArgsRequestReadBase(
            ModbusFunctionCodes enumCode,
            ushort startAddr,
            ushort quantity
        ) : base(enumCode)
            => Init(
                startAddr,
                quantity,
                out _startingAddress,
                out _quantityOfItems
            );

        #endregion

        #region Properties

        public override IReadOnlyList<byte> RawData => [
            StartingAddress.MSB(),
            StartingAddress.LSB(),
            QuantityOfItems.MSB(),
            QuantityOfItems.LSB(),
        ];

        private readonly ushort _startingAddress;
        public ushort StartingAddress => _startingAddress;

        private readonly ushort _quantityOfItems;
        protected ushort QuantityOfItems => _quantityOfItems;

        protected abstract ushort MinQuantity { get; }

        protected abstract ushort MaxQuantity { get; }

        #endregion

        #region Init

        protected virtual void InitStartingAddress(
            ushort address,
            out ushort startingAddress
        ) => startingAddress = address;

        protected virtual void InitQuantityOfItems(
            ushort quantity,
            out ushort quantityOfItems
        )
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(quantity, MinQuantity);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(quantity, MaxQuantity);

            quantityOfItems = quantity;
        }

        private void Init(
            ushort address,
            ushort quantity,
            out ushort startingAddress,
            out ushort quantityOfItems
        )
        {
            InitStartingAddress(address, out startingAddress);
            InitQuantityOfItems(quantity, out quantityOfItems);
        }

        #endregion
    }
}
