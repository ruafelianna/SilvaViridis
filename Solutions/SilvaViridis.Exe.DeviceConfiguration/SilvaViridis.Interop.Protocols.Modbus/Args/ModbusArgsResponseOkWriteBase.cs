using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using Andromeda.Numerics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace SilvaViridis.Interop.Protocols.Modbus.Args
{
    public abstract class ModbusArgsResponseOkWriteBase<TRequest, TResponse> :
        ModbusArgsBase, IModbusArgsResponseOk
        where TRequest : IModbusArgsRequest
        where TResponse : ModbusArgsResponseOkWriteBase<TRequest, TResponse>
    {
        protected ModbusArgsResponseOkWriteBase(
            TRequest request,
            IReadOnlyList<byte> bytes
        ) : base(request.RawFunctionCode)
            => Init(
                request,
                bytes,
                out _regAddress,
                out _dataValue
            );

        protected static async Task<TResponse> Create(
            Func<TRequest, IReadOnlyList<byte>, TResponse> createInstance,
            TRequest request,
            DGetBytes getBytes,
            CancellationToken token = default
        )
        {
            var bytes = await getBytes(4, token);

            return createInstance(request, bytes);
        }

        public override IReadOnlyList<byte> RawData => [
            RegAddress.Byte1(),
            RegAddress.Byte2(),
            DataValue.Byte1(),
            DataValue.Byte2(),
        ];

        private readonly ushort _regAddress;
        protected ushort RegAddress => _regAddress;

        private readonly ushort _dataValue;
        protected ushort DataValue => _dataValue;

        protected virtual void InitRegAddress(
            TRequest request,
            IReadOnlyList<byte> data,
            out ushort regAddress
        ) => regAddress = Helpers.AsUShort(data[0], data[1]);

        protected virtual void InitDataValue(
            TRequest request,
            IReadOnlyList<byte> data,
            out ushort dataValue
        ) => dataValue = Helpers.AsUShort(data[2], data[3]);

        private void Init(
            TRequest request,
            IReadOnlyList<byte> data,
            out ushort regAddress,
            out ushort dataValue
        )
        {
            InitRegAddress(request, data, out regAddress);
            InitDataValue(request, data, out dataValue);
        }
    }
}
