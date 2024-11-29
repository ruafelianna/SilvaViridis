using SilvaViridis.Common.Checksums;
using SilvaViridis.Common.Checksums.Enums;
using SilvaViridis.Common.Numerics;
using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.Extensions
{
    public static class ModbusRTUExtensions
    {
        public static byte[] RTU_Main(
            this IModbusArgs args,
            byte address
        ) => [
            address,
            args.RawFunctionCode,
            .. args.RawData,
        ];

        public static ushort RTU_CRC(
            this IModbusArgs args,
            byte address
        ) => args.RTU_Main(address).GetCRC16(CRC16Type.ModbusRTU);

        public static ushort RTU_CRC(
            this IReadOnlyList<byte> main
        ) => main.GetCRC16(CRC16Type.ModbusRTU);

        public static byte[] RTU_Cmd(
            this IModbusArgs args,
            byte address
        )
        {
            var main = args.RTU_Main(address);
            var crc = main.RTU_CRC();

            return [
                .. main,
                crc.LSB(),
                crc.MSB(),
            ];
        }
    }
}
