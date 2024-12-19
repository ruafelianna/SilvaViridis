using SilvaViridis.Common.Numerics;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC03_ReadHoldingRegisters
{
    public class ArgsResponseOk_03 :
        ModbusArgsResponseOkReadBase<IArgsRequest_03, ArgsResponseOk_03>,
        IArgsResponseOk_03
    {
        protected ArgsResponseOk_03(
            IArgsRequest_03 request,
            IReadOnlyList<byte> data
        ) : base(request, data)
            => Init(
                request,
                data,
                out _startingAddress,
                out _quantityOfRegisters
            );

        public static async Task<IArgsResponseOk_03> Create(
            IArgsRequest_03 request,
            DGetBytes getBytes,
            CancellationToken token = default
        ) => await Create(
            (request, bytes) => new ArgsResponseOk_03(request, bytes),
            request,
            getBytes,
            token
        );

        public IReadOnlyList<byte> RegisterValue => Bytes;

        private readonly ushort _startingAddress;
        public ushort StartingAddress => _startingAddress;

        private readonly ushort _quantityOfRegisters;
        public ushort QuantityOfRegisters => _quantityOfRegisters;

        public IReadOnlyList<ushort> RegistersDirect
            => RegisterValue.AsUShorts(true);

        public IReadOnlyList<ushort> RegistersReverse
            => RegisterValue.AsUShorts(false);

        protected override void InitByteCount(
            IArgsRequest_03 request,
            IReadOnlyList<byte> data,
            out byte byteCount
        )
        {
            base.InitByteCount(request, data, out byteCount);

            ArgumentOutOfRangeException.ThrowIfNotEqual(
                request.QuantityOfRegisters << 1,
                byteCount
            );
        }

        protected virtual void InitStartingAddress(
            IArgsRequest_03 request,
            IReadOnlyList<byte> data,
            out ushort startingAddress
        ) => startingAddress = request.StartingAddress;

        protected virtual void InitQuantityOfRegisters(
            IArgsRequest_03 request,
            IReadOnlyList<byte> data,
            out ushort quantityOfRegisters
        ) => quantityOfRegisters = request.QuantityOfRegisters;

        private void Init(
            IArgsRequest_03 request,
            IReadOnlyList<byte> data,
            out ushort startingAddress,
            out ushort quantityOfRegisters
        )
        {
            InitStartingAddress(request, data, out startingAddress);
            InitQuantityOfRegisters(request, data, out quantityOfRegisters);
        }
    }
}
