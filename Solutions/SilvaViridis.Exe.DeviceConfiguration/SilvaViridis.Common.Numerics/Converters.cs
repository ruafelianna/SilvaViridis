using System.Collections.Generic;

namespace SilvaViridis.Common.Numerics
{
    public static class Converters
    {
        public static ushort AsUShort(this (byte b1, byte b2) b)
            => unchecked((ushort)((b.b1 << 8) | b.b2));

        public static ushort[] AsUShorts(
            this IReadOnlyList<byte> bytes,
            bool isDirect = true)
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
    }
}
