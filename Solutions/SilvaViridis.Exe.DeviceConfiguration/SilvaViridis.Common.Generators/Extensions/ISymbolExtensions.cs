using Microsoft.CodeAnalysis;
using SilvaViridis.Common.Text.Extensions;

namespace SilvaViridis.Common.Generators.Extensions
{
    public static class ISymbolExtensions
    {
        public static string FullNS(this ISymbol symbol)
            => symbol.ContainingNamespace
                .ToDisplayString(
                    SymbolDisplayFormat.FullyQualifiedFormat
                )
                .TrimStart(PRE_Global);
    }
}
