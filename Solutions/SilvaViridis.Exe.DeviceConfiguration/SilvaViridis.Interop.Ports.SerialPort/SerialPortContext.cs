using SilvaViridis.Interop.Ports.SerialPort.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MS_SerialPort = System.IO.Ports.SerialPort;

namespace SilvaViridis.Interop.Ports.SerialPort
{
    public class SerialPortContext : ISerialPortContext
    {
        public static Task<IEnumerable<string>> Ports
            => Task.Run(() => MS_SerialPort.GetPortNames().AsEnumerable());
    }
}
