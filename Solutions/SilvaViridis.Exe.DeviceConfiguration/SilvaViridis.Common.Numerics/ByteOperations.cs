namespace SilvaViridis.Common.Numerics
{
    public static class ByteOperations
    {
        public const int ByteSize = 8;

        public const int Log_2_ByteSize = 3;

        public const int ByteStartIndex = 0;

        public const int ByteStopIndex = ByteSize - 1;

        public static byte ModByBitsInByte(this int number)
            => unchecked((byte)(number & ByteStopIndex));

        public static bool[] AsBools(this byte b, int num = ByteSize)
        {
            var result = new bool[num];

            for (var i = 0; i < num; i++)
            {
                result[i] = b.IsOdd();
                b >>= 1;
            }

            return result;
        }

        #region UShort

        public static byte MSB(this ushort number)
            => unchecked((byte)(number >> ByteSize));

        public static byte LSB(this ushort number)
            => unchecked((byte)(number & byte.MaxValue));

        public static byte Byte1(this ushort number)
            => number.MSB();

        public static byte Byte2(this ushort number)
            => number.LSB();

        public static int FullBytes(this ushort bits)
            => bits >> Log_2_ByteSize;

        public static int ContainingBytes(this ushort bits)
            => (bits + ByteStopIndex) >> Log_2_ByteSize;

        public static byte ByteRemainder(this ushort bits)
            => unchecked((byte)(bits & ByteStopIndex));

        #endregion

        #region Int

        public static int FullBytes(this int bits)
            => bits >> Log_2_ByteSize;

        #endregion
    }
}
