using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SilvaViridis.Common.Numerics
{
    public class NumberRegex
    {
        public NumberRegex(
            NumberFormatInfo numberFormat,
            NumberStyles numberStyles
        )
        {
            NumberFormat = numberFormat;

            NumberStyles = numberStyles;

            var decimalSep = Regex.Escape(numberFormat.NumberDecimalSeparator);

            var groupSep = numberFormat.NumberGroupSeparator;

            if (groupSep == "\u00A0")
            {
                groupSep = $"( |{groupSep})";
            }
            else
            {
                groupSep = Regex.Escape(groupSep);
            }

            var groupCnt = numberFormat.NumberGroupSizes.Length;

            var addPart2 =
                groupCnt > 1
                || (
                    groupCnt == 1
                    && numberFormat.NumberGroupSizes[0] != 0
                );

            var numBuilder = new StringBuilder();

            numBuilder.Append(@"[+\-]?");

            if (addPart2)
            {
                numBuilder.Append('(');
            }

            numBuilder.Append("[0-9]+");

            if (addPart2)
            {
                numBuilder.Append('|');

                var firstGroupSize = numberFormat.NumberGroupSizes[0];

                var times = firstGroupSize == 1 ? "1" : $"1,{firstGroupSize}";

                numBuilder.Append($"[0-9]{{{times}}}");
                numBuilder.Append($"({groupSep}[0-9]{{{firstGroupSize}}})*");

                foreach (var groupSize in numberFormat
                    .NumberGroupSizes
                    .Reverse()
                    .Skip(1)
                )
                {
                    numBuilder.Append(
                        $"{groupSep}[0-9]{{{groupSize}}}"
                    );
                }

                numBuilder.Append(')');
            }

            IntNumberPattern = $"^{numBuilder}$";

            numBuilder.Append(@$"({decimalSep}[0-9]+)?");
            numBuilder.Append(@"([eE][+\-]?[0-9]+)?");

            NumberPattern = $"^{numBuilder}$";
        }

        public NumberFormatInfo NumberFormat { get; }

        public NumberStyles NumberStyles { get; }

        public string NumberPattern { get; }

        public string IntNumberPattern { get; }

        public bool IsNumber(string value)
            => Regex.IsMatch(value, NumberPattern);

        public bool IsIntNumber(string value)
            => Regex.IsMatch(value, IntNumberPattern);
    }
}
