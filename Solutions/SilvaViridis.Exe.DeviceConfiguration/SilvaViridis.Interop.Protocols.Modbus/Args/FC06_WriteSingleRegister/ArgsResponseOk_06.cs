using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.FC06_WriteSingleRegister
{
    public class ArgsResponseOk_06 :
        ModbusArgsResponseOkWriteBase<IArgsRequest_06, ArgsResponseOk_06>,
        IArgsResponseOk_06
    {
        protected ArgsResponseOk_06(
            IArgsRequest_06 request,
            IReadOnlyList<byte> data
        ) : base(request, data)
        {
        }

        public static async Task<IArgsResponseOk_06> Create(
            IArgsRequest_06 request,
            DGetBytes getBytes,
            CancellationToken token = default
        ) => await Create(
            (request, bytes) => new ArgsResponseOk_06(request, bytes),
            request,
            getBytes,
            token
        );

        public ushort RegisterAddress => RegAddress;

        public ushort RegisterValue => DataValue;

        protected override void InitRegAddress(
            IArgsRequest_06 request,
            IReadOnlyList<byte> data,
            out ushort regAddress
        )
        {
            base.InitRegAddress(request, data, out regAddress);

            ArgumentOutOfRangeException.ThrowIfNotEqual(
                regAddress,
                request.RegisterAddress
            );
        }

        protected override void InitDataValue(
            IArgsRequest_06 request,
            IReadOnlyList<byte> data,
            out ushort dataValue
        )
        {
            base.InitDataValue(request, data, out dataValue);

            ArgumentOutOfRangeException.ThrowIfNotEqual(
                dataValue,
                request.RegisterValue
            );
        }
    }
}
