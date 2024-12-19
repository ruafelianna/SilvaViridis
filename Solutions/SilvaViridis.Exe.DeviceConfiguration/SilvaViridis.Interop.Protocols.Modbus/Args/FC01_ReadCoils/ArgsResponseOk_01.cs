using SilvaViridis.Common.Numerics;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC01_ReadCoils
{
    public class ArgsResponseOk_01 :
        ModbusArgsResponseOkReadBase<IArgsRequest_01, ArgsResponseOk_01>,
        IArgsResponseOk_01
    {
        protected ArgsResponseOk_01(
            IArgsRequest_01 request,
            IReadOnlyList<byte> data
        ) : base(request, data)
            => Init(
                request,
                data,
                out _startingAddress,
                out _quantityOfCoils
            );

        public static async Task<IArgsResponseOk_01> Create(
            IArgsRequest_01 request,
            DGetBytes getBytes,
            CancellationToken token = default
        ) => await Create(
            (request, bytes) => new ArgsResponseOk_01(request, bytes),
            request,
            getBytes,
            token
        );

        public IReadOnlyList<byte> CoilStatus => Bytes;

        private readonly ushort _startingAddress;
        public ushort StartingAddress => _startingAddress;

        private readonly ushort _quantityOfCoils;
        public ushort QuantityOfCoils => _quantityOfCoils;

        public IReadOnlyList<bool> Coils =>
        [
            .. CoilStatus
                .Take(QuantityOfCoils.FullBytes())
                .Select(b => b.AsBools())
                .SelectMany(x => x),
            .. CoilStatus
                .TakeLast(ByteCount - QuantityOfCoils.FullBytes())
                .Select(b => b.AsBools(QuantityOfCoils.ByteRemainder()))
                .SelectMany(x => x)
        ];

        protected override void InitByteCount(
            IArgsRequest_01 request,
            IReadOnlyList<byte> data,
            out byte byteCount
        )
        {
            base.InitByteCount(request, data, out byteCount);

            ArgumentOutOfRangeException.ThrowIfNotEqual(
                request.QuantityOfCoils.ContainingBytes(),
                byteCount
            );
        }

        protected virtual void InitStartingAddress(
            IArgsRequest_01 request,
            IReadOnlyList<byte> data,
            out ushort startingAddress
        ) => startingAddress = request.StartingAddress;

        protected virtual void InitQuantityOfCoils(
            IArgsRequest_01 request,
            IReadOnlyList<byte> data,
            out ushort quantityOfCoils
        ) => quantityOfCoils = request.QuantityOfCoils;

        private void Init(
            IArgsRequest_01 request,
            IReadOnlyList<byte> data,
            out ushort startingAddress,
            out ushort quantityOfCoils
        )
        {
            InitStartingAddress(request, data, out startingAddress);
            InitQuantityOfCoils(request, data, out quantityOfCoils);
        }
    }
}
