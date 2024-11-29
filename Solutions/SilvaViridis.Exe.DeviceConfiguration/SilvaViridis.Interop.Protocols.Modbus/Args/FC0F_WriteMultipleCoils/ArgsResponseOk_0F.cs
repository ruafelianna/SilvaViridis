using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC0F_WriteMultipleCoils
{
    public class ArgsResponseOk_0F :
        ModbusArgsResponseOkWriteBase<IArgsRequest_0F, ArgsResponseOk_0F>,
        IArgsResponseOk_0F
    {
        protected ArgsResponseOk_0F(
            IArgsRequest_0F request,
            IReadOnlyList<byte> data
        ) : base(request, data)
        {
        }

        public static async Task<IArgsResponseOk_0F> Create(
            IArgsRequest_0F request,
            DGetBytes getBytes,
            CancellationToken token = default
        ) => await Create(
            (request, bytes) => new ArgsResponseOk_0F(request, bytes),
            request,
            getBytes,
            token
        );

        public ushort StartingAddress => RegAddress;

        public ushort QuantityOfOutputs => DataValue;

        protected override void InitRegAddress(
            IArgsRequest_0F request,
            IReadOnlyList<byte> data,
            out ushort regAddress
        )
        {
            base.InitRegAddress(request, data, out regAddress);

            ArgumentOutOfRangeException.ThrowIfNotEqual(
                regAddress,
                request.StartingAddress
            );
        }

        protected override void InitDataValue(
            IArgsRequest_0F request,
            IReadOnlyList<byte> data,
            out ushort dataValue
        )
        {
            base.InitDataValue(request, data, out dataValue);

            ArgumentOutOfRangeException.ThrowIfNotEqual(
                dataValue,
                request.QuantityOfOutputs
            );
        }
    }
}
