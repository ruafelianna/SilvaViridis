using SilvaViridis.Common.Checksums;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.Extensions
{
    public static class ModbusASCIIExtensions
    {
        public static byte[] ASCII_Main(
            this IModbusArgs args,
            byte address
        ) => [
            .. Encode(address),
            .. Encode(args.RawFunctionCode),
            .. Encode(args.RawData),
        ];

        public static byte ASCII_LRC(
            this IModbusArgs args,
            byte address
        ) => args.ASCII_Main(address).GetLRC();

        public static byte ASCII_LRC(
            this IReadOnlyList<byte> main
        ) => main.GetLRC();

        public static byte[] ASCII_Cmd(
            this IModbusArgs args,
            byte address
        )
        {
            var main = args.ASCII_Main(address);

            return [
                0x3A,
                .. main,
                .. Encode(main.ASCII_LRC()),
                0x0D,
                0x0A,
            ];
        }

        private static byte[] Encode(byte b)
            => Encoding.ASCII.GetBytes($"{b:X2}");

        private static byte[] Encode(IEnumerable<byte> bytes)
            => bytes.Select(Encode).SelectMany(x => x).ToArray();
    }
}
