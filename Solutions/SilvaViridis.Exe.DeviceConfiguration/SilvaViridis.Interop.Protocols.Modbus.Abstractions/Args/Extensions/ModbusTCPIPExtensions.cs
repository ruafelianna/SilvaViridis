using SilvaViridis.Common.Numerics;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.Extensions
{
    public static class ModbusTCPIPExtensions
    {
        public static byte[] TCPIP_Cmd(
            this IModbusArgs args,
            ushort id,
            byte address,
            ushort protocol = 0
        )
        {
            var main = args.RTU_Main(address);
            var length = unchecked((ushort)main.Length);

            return [
                id.MSB(),
                id.LSB(),
                protocol.MSB(),
                protocol.LSB(),
                length.MSB(),
                length.LSB(),
                .. main
            ];
        }
    }
}
