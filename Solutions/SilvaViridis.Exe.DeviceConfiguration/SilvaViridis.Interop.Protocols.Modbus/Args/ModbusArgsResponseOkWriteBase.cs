using SilvaViridis.Common.Numerics;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
            RegAddress.MSB(),
            RegAddress.LSB(),
            DataValue.MSB(),
            DataValue.LSB(),
        ];

        private readonly ushort _regAddress;
        protected ushort RegAddress => _regAddress;

        private readonly ushort _dataValue;
        protected ushort DataValue => _dataValue;

        protected virtual void InitRegAddress(
            TRequest request,
            IReadOnlyList<byte> data,
            out ushort regAddress
        ) => regAddress = (data[0], data[1]).AsUShort();

        protected virtual void InitDataValue(
            TRequest request,
            IReadOnlyList<byte> data,
            out ushort dataValue
        ) => dataValue = (data[2], data[3]).AsUShort();

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
