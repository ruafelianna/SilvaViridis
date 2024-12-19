using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC10_WriteMultipleRegisters
{
    public class ArgsResponseOk_10 :
        ModbusArgsResponseOkWriteBase<IArgsRequest_10, ArgsResponseOk_10>,
        IArgsResponseOk_10
    {
        protected ArgsResponseOk_10(
            IArgsRequest_10 request,
            IReadOnlyList<byte> data
        ) : base(request, data)
        {
        }

        public static async Task<IArgsResponseOk_10> Create(
            IArgsRequest_10 request,
            DGetBytes getBytes,
            CancellationToken token = default
        ) => await Create(
            (request, bytes) => new ArgsResponseOk_10(request, bytes),
            request,
            getBytes,
            token
        );

        public ushort StartingAddress => RegAddress;

        public ushort QuantityOfRegisters => DataValue;

        protected override void InitRegAddress(
            IArgsRequest_10 request,
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
            IArgsRequest_10 request,
            IReadOnlyList<byte> data,
            out ushort dataValue
        )
        {
            base.InitDataValue(request, data, out dataValue);

            ArgumentOutOfRangeException.ThrowIfNotEqual(
                dataValue,
                request.QuantityOfRegisters
            );
        }
    }
}
