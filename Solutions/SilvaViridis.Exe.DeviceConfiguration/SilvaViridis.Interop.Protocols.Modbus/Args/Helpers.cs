using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Args
{
    internal static class Helpers
    {
        public static ushort[] AsUShorts(
            IReadOnlyList<byte> bytes, bool isDirect = true)
        {
            var result = new ushort[bytes.Count >> 1];
            var count = 0;
            foreach (var b in bytes)
            {
                if (
                    isDirect && (count & 1) == 0
                    || !isDirect && (count & 1) == 1
                )
                {
                    result[count >> 1] |= (ushort)(b << 8);
                }
                else
                {
                    result[count >> 1] |= b;
                }
                count++;
            }
            return result;
        }

        public static ushort AsUShort(byte b1, byte b2)
            => unchecked((ushort)((b1 << 8) | b2));
    }
}
