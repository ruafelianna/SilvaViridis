using System.Collections.Generic;

namespace SilvaViridis.Common.Checksums
{
    public static class LRC
    {
        public static byte GetLRC(this IEnumerable<byte> buffer)
        {
            int lrc = 0;
            foreach (byte b in buffer)
            {
                lrc = lrc + b & 0xFF;
            }
            lrc = (lrc ^ 0xFF) + 1 & 0xFF;
            return (byte)lrc;
        }
    }
}
