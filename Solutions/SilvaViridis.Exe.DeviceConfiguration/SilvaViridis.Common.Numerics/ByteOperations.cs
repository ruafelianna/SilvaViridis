namespace SilvaViridis.Common.Numerics
{
    public static class ByteOperations
    {
        public const int ByteSize = 8;

        #region UShort

        public static byte MSB(this ushort number)
            => unchecked((byte)(number >> ByteSize));

        public static byte LSB(this ushort number)
            => unchecked((byte)(number & byte.MaxValue));

        public static byte Byte1(this ushort number)
            => number.MSB();

        public static byte Byte2(this ushort number)
            => number.LSB();

        #endregion
    }
}
