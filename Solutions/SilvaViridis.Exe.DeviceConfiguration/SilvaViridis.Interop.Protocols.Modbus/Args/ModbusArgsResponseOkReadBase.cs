using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Args
{
    public abstract class ModbusArgsResponseOkReadBase<TRequest, TResponse> :
        ModbusArgsBase, IModbusArgsResponseOk
        where TRequest : IModbusArgsRequest
        where TResponse : ModbusArgsResponseOkReadBase<TRequest, TResponse>
    {
        protected ModbusArgsResponseOkReadBase(
            TRequest request,
            IReadOnlyList<byte> data
        ) : base(request.RawFunctionCode)
            => Init(
                request,
                data,
                out _byteCount,
                out _bytes
            );

        protected static async Task<TResponse> Create(
            Func<TRequest, IReadOnlyList<byte>, TResponse> createInstance,
            TRequest request,
            DGetBytes getBytes,
            CancellationToken token = default
        )
        {
            var byteCount = (await getBytes(1, token)).Single();
            var data = await getBytes(byteCount, token);

            return createInstance(request, data);
        }

        public override IReadOnlyList<byte> RawData => [
            ByteCount,
            .. Bytes,
        ];

        private readonly byte _byteCount;
        public byte ByteCount => _byteCount;

        private readonly IReadOnlyList<byte> _bytes;
        protected IReadOnlyList<byte> Bytes => _bytes;

        protected virtual void InitByteCount(
            TRequest request,
            IReadOnlyList<byte> data,
            out byte byteCount
        )
        {
            var count = data.Count;

            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, byte.MaxValue);

            byteCount = unchecked((byte)count);
        }

        protected virtual void InitBytes(
            TRequest request,
            IReadOnlyList<byte> data,
            out IReadOnlyList<byte> bytes
        ) => bytes = data;

        private void Init(
            TRequest request,
            IReadOnlyList<byte> data,
            out byte byteCount,
            out IReadOnlyList<byte> bytes
        )
        {
            InitByteCount(request, data, out byteCount);
            InitBytes(request, data, out bytes);
        }
    }
}
