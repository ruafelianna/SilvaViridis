using System;
using System.Collections.Generic;
using System.Globalization;

namespace SilvaViridis.Common.Numerics
{
    public static class Converters
    {
        #region Byte

        public static byte? AsByte(
            this string? value,
            IFormatProvider provider,
            NumberStyles numberStyles = NumberStyles.None
        ) => byte.TryParse(value, numberStyles, provider, out var result)
                ? result
                : null;

        public static byte? AsByte(
            this string? value,
            NumberRegex numberRegex
        ) => value.AsByte(numberRegex.NumberFormat, numberRegex.NumberStyles);

        #endregion

        #region UShort

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

        #endregion

        #region Int

        public static int? AsInt(
            this string? value,
            IFormatProvider provider,
            NumberStyles numberStyles = NumberStyles.None
        ) => int.TryParse(value, numberStyles, provider, out var result)
                ? result
                : null;

        public static int? AsInt(
            this string? value,
            NumberRegex numberRegex
        ) => value.AsInt(numberRegex.NumberFormat, numberRegex.NumberStyles);

        #endregion
    }
}
