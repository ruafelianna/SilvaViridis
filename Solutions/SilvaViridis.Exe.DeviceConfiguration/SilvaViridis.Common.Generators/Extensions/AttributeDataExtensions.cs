using Microsoft.CodeAnalysis;
using SilvaViridis.Common.Text.Extensions;
using System;
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

            if (attrValue.Key is null)
            {
                return default;
            }

            var objType = typeof(T);
            var value = attrValue.Value.Value;

            if (
                objType.IsEnum
                && objType.IsEnumDefined(value)
            )
            {
                return (T)Enum.ToObject(objType, value);
            }
            else if (value is T result)
            {
                if (
                    result is string str
                    && checkStringEmpty
                    && str.IsNullOrWhiteSpace()
                )
                {
                    return default;
                }

                return result;
            }

            return default;
        }
    }
}
