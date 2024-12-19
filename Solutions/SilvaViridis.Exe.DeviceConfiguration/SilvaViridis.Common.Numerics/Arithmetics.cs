namespace SilvaViridis.Common.Numerics
{
    public static class Arithmetics
    {
        #region IsEven

        public static bool IsEven(this byte number)
            => (number & 1) == 0;

        public static bool IsEven(this sbyte number)
            => (number & 1) == 0;

        public static bool IsEven(this ushort number)
            => (number & 1) == 0;

        public static bool IsEven(this short number)
            => (number & 1) == 0;

        public static bool IsEven(this uint number)
            => (number & 1) == 0;

        public static bool IsEven(this int number)
            => (number & 1) == 0;

        public static bool IsEven(this ulong number)
            => (number & 1) == 0;

        public static bool IsEven(this long number)
            => (number & 1) == 0;

        #endregion

        #region IsOdd

        public static bool IsOdd(this byte number)
            => (number & 1) == 1;

        public static bool IsOdd(this sbyte number)
            => (number & 1) == 1;

        public static bool IsOdd(this ushort number)
            => (number & 1) == 1;

        public static bool IsOdd(this short number)
            => (number & 1) == 1;

        public static bool IsOdd(this uint number)
            => (number & 1) == 1;

        public static bool IsOdd(this int number)
            => (number & 1) == 1;

        public static bool IsOdd(this ulong number)
            => (number & 1) == 1;

        public static bool IsOdd(this long number)
            => (number & 1) == 1;

        #endregion
    }
}
