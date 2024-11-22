using Microsoft.CodeAnalysis;
using SilvaViridis.Common.Text.Extensions;
using System.Linq;

namespace SilvaViridis.Common.Generators.Extensions
{
    public static class AttributeDataExtensions
    {
        public static Optional<T> GetNamedValue<T>(
            this AttributeData attr,
            string key,
            bool checkStringEmpty = true
        )
        {
            var attrValue = attr.NamedArguments
                .FirstOrDefault(
                    arg => arg.Key == key
                );

            if (attrValue.Value.Value is T value)
            {
                if (
                    value is string str
                    && checkStringEmpty
                    && str.IsNullOrWhiteSpace()
                )
                {
                    return default;
                }

                return value;
            }

            return default;
        }
    }
}
