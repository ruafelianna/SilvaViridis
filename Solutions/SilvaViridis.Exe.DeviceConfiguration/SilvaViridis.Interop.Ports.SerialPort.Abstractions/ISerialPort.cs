using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace SilvaViridis.Interop.Ports.SerialPort.Abstractions
{
    public interface ISerialPort
    {
        Task<IReadOnlyList<byte>> ReadAsync(
            int count,
            CancellationToken token = default
        );

        Task WriteAsync(
            IReadOnlyList<byte> buffer,
            CancellationToken token = default
        );
    }
}
