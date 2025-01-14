using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Ports.SerialPort.Abstractions
{
    public interface ISerialPortContext
    {
        abstract static Task<IEnumerable<string>> Ports { get; }
    }
}
