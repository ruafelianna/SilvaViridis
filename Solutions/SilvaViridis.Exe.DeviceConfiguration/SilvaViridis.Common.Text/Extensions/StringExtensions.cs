using System.Diagnostics.CodeAnalysis;

namespace SilvaViridis.Common.Text.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(
            [NotNullWhen(false)] this string? str
        ) => string.IsNullOrEmpty(str);

        public static bool IsNullOrWhiteSpace(
            [NotNullWhen(false)] this string? str
        ) => string.IsNullOrWhiteSpace(str);

        public static string TrimStart(
            this string str,
            string start
        ) => str.StartsWith(start)
            ? str[start.Length..]
            : str;

        public static string Format(
            this string? str,
            params object?[]? args
        ) => string.Format(str, args);
    }
}
