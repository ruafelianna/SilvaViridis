using Microsoft.CodeAnalysis;

namespace SilvaViridis.Common.Localization.Generators
{
    internal class AnalyzerRules
    {
        private const string _Error_ConfigValueNotProvided
            = "SVCLG0001";

        private const string _Category_LocalizationConfig
            = "LocalizationConfig";

        public static readonly DiagnosticDescriptor ErrorNoClass
            = ErrorConfValueNotSet(
                LocalizationConf_Class
            );

        public static readonly DiagnosticDescriptor ErrorNoDefault
            = ErrorConfValueNotSet(
                LocalizationConf_Default
            );

        public static readonly DiagnosticDescriptor ErrorNoNamespace
            = ErrorConfValueNotSet(
                LocalizationConf_Namespace
            );

        private static DiagnosticDescriptor ErrorConfValueNotSet(
            string value
        ) => new(
            id: _Error_ConfigValueNotProvided,
            title: Strings.ConfValueNotFound,
            messageFormat: string.Format(
                Strings.ValueIsNotProvided,
                value
            ),
            category: _Category_LocalizationConfig,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
    }
}
