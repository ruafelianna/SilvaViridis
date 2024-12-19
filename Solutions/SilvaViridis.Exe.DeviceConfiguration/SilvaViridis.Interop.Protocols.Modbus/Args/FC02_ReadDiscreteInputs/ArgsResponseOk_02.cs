using SilvaViridis.Common.Numerics;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC02_ReadDiscreteInputs
{
    public class ArgsResponseOk_02 :
        ModbusArgsResponseOkReadBase<IArgsRequest_02, ArgsResponseOk_02>,
        IArgsResponseOk_02
    {
        protected ArgsResponseOk_02(
            IArgsRequest_02 request,
            IReadOnlyList<byte> data
        ) : base(request, data)
            => Init(
                request,
                data,
                out _startingAddress,
                out _quantityOfInputs
            );

        public static async Task<IArgsResponseOk_02> Create(
            IArgsRequest_02 request,
            DGetBytes getBytes,
            CancellationToken token = default
        ) => await Create(
            (request, bytes) => new ArgsResponseOk_02(request, bytes),
            request,
            getBytes,
            token
        );

        public IReadOnlyList<byte> InputStatus => Bytes;

        private readonly ushort _startingAddress;
        public ushort StartingAddress => _startingAddress;

        private readonly ushort _quantityOfInputs;
        public ushort QuantityOfInputs => _quantityOfInputs;

        public IReadOnlyList<bool> Inputs =>
        [
            .. InputStatus
                .Take(QuantityOfInputs.FullBytes())
                .Select(b => b.AsBools())
                .SelectMany(x => x),
            .. InputStatus
                .TakeLast(ByteCount - QuantityOfInputs.FullBytes())
                .Select(b => b.AsBools(QuantityOfInputs.ByteRemainder()))
                .SelectMany(x => x)
        ];

        protected override void InitByteCount(
            IArgsRequest_02 request,
            IReadOnlyList<byte> data,
            out byte byteCount
        )
        {
            base.InitByteCount(request, data, out byteCount);

            ArgumentOutOfRangeException.ThrowIfNotEqual(
                request.QuantityOfInputs.ContainingBytes(),
                byteCount
            );
        }

        protected virtual void InitStartingAddress(
            IArgsRequest_02 request,
            IReadOnlyList<byte> data,
            out ushort startingAddress
        ) => startingAddress = request.StartingAddress;

        protected virtual void InitQuantityOfInputs(
            IArgsRequest_02 request,
            IReadOnlyList<byte> data,
            out ushort quantityOfInputs
        ) => quantityOfInputs = request.QuantityOfInputs;

        private void Init(
            IArgsRequest_02 request,
            IReadOnlyList<byte> data,
            out ushort startingAddress,
            out ushort quantityOfInputs
        )
        {
            InitStartingAddress(request, data, out startingAddress);
            InitQuantityOfInputs(request, data, out quantityOfInputs);
        }
    }
}
