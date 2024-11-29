using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args
{
    public delegate Task<IReadOnlyList<byte>> DGetBytes(
        int count,
        CancellationToken cancellationToken
    );

    public delegate Task<TResponse> DCreateResponse<TRequest, TResponse>(
        TRequest requst,
        DGetBytes getBytes,
        CancellationToken cancellationToken
    )
        where TRequest : IModbusArgsRequest
        where TResponse : IModbusArgsResponseOk;

    public delegate TError DCreateError<TError>(
        byte funcCode,
        byte exCode
    )
        where TError : IModbusArgsResponseError;
}
