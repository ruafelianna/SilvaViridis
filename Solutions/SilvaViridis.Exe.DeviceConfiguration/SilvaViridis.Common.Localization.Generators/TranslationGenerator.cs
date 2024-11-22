using LanguageExt;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using Tommy;

namespace SilvaViridis.Common.Localization.Generators
{
    [Generator(LanguageNames.CSharp)]
    public class TranslationGenerator : IIncrementalGenerator
    {
        public void Initialize(
            IncrementalGeneratorInitializationContext context
        )
        {
            var config = context.AdditionalTextsProvider
                .Where(IsLocalizationConfig)
                .Select(ParseLocalizationConfig);

            var tomls = context.AdditionalTextsProvider
                .Where(IsTomlFile)
                .Collect();

            var source = config
                .Combine(tomls)
                .Combine(context.AnalyzerConfigOptionsProvider)
                .Select(ParseDefaultLocaleFile);

            context.RegisterSourceOutput(source, (ctx, valueOrError)
                => valueOrError.Match(
                    error => ctx.ReportDiagnostic(
                        Diagnostic.Create(
                            error.Descriptor,
                            error.Location,
                            error.FilePath
                        )
                    ),
                    conf => ctx.AddSource(
                        $"{conf.GenNamespace}.{conf.GenClass}{EXT_GeneratedCSharp}",
                        GenerateSource(conf)
                    )
                )
            );
        }

        private record LocalizationConfig(
            string ConfFileDirectory,
            string DefaultLocaleFileFullPath,
            string GenClass,
            string DefaultLocale,
            string GenNamespace
        );

        private record DefaultConfig(
            string GenClass,
            string DefaultLocale,
            string GenNamespace,
            IEnumerable<string> TranslationStrings,
            string? TranslationPath
        );

        private record Error(
            DiagnosticDescriptor Descriptor,
            string FilePath
        )
        {
            public Location Location => Location.Create(
                FilePath,
                TextSpan.FromBounds(0, 0),
                new LinePositionSpan()
            );
        }

        private static bool IsLocalizationConfig(
            AdditionalText file
        ) => Path.GetFileName(file.Path) == LocalizationConf;

        private static bool IsTomlFile(
            AdditionalText file
        ) => file.Path.EndsWith(LocalizationConf_Ext);

        private static Either<LocalizationConfig, Error> ParseLocalizationConfig(
            AdditionalText file,
            CancellationToken token
        )
        {
            var toml = TOML.Parse(
                new StringReader(file.GetText(token)?.ToString())
            );

            var desc = !toml.HasKey(LocalizationConf_Class)
                ? AnalyzerRules.ErrorNoClass
                : !toml.HasKey(LocalizationConf_Default)
                    ? AnalyzerRules.ErrorNoDefault
                    : !toml.HasKey(LocalizationConf_Namespace)
                        ? AnalyzerRules.ErrorNoNamespace
                        : null;

            if (desc is not null)
            {
                return new Error(desc, file.Path);
            }

            var confDir = Path.GetDirectoryName(file.Path);

            var defaultLocaleFile = Path.Combine(
                confDir,
                $"{toml[LocalizationConf_Default]}{LocalizationConf_Ext}"
            );

            return new LocalizationConfig(
                confDir,
                defaultLocaleFile,
                toml[LocalizationConf_Class],
                toml[LocalizationConf_Default],
                toml[LocalizationConf_Namespace]
            );
        }

        private static Either<DefaultConfig, Error> ParseDefaultLocaleFile(
            (
                (
                    Either<LocalizationConfig, Error> Left,
                    ImmutableArray<AdditionalText> Right
                ) Left,
                AnalyzerConfigOptionsProvider Right
            ) data,
            CancellationToken token
        )
        {
            var ((localeConfigOrError, translationFiles), analyzerOptions)
                = data;

            return localeConfigOrError.Match<Either<DefaultConfig, Error>>(
                error => error,
                localeConfig => {
                    analyzerOptions.GlobalOptions.TryGetValue(
                        AnalyzerOptions_Project_Dir,
                        out var projectDir
                    );

                    var translationStrings = TOML
                        .Parse(
                            new StringReader(translationFiles
                                .FirstOrDefault(file =>
                                    file.Path == localeConfig.DefaultLocaleFileFullPath
                                )?
                                .GetText(token)?
                                .ToString()
                                ?? string.Empty
                            )
                        )
                        .AsTable
                        .RawTable
                        .Select(t => t.Key);

                    var translationPath = localeConfig.ConfFileDirectory
                        .Substring((projectDir?.Length ?? 1) - 1);

                    return new DefaultConfig(
                        localeConfig.GenClass,
                        localeConfig.DefaultLocale,
                        localeConfig.GenNamespace,
                        translationStrings,
                        translationPath
                    );
                }
            );
        }

        private static string GenerateSource(DefaultConfig config)
        {
            var translationUnits = config.TranslationStrings
                .Select(t =>
                    @$"{Tab}{Tab}{Tab}{t} = {KW_New} {CL_TranslationUnit}(
{Tab}{Tab}{Tab}{Tab}{CL_TranslationProvider},
{Tab}{Tab}{Tab}{Tab}{KW_NameOf}({t})
{Tab}{Tab}{Tab});"
                );

            var translationProperties = config.TranslationStrings
                .Select(t =>
                    $"{Tab}{Tab}{KW_Public} {KW_Static} {I_TranslationUnit} {t} {{ {KW_Get}; }}"
                );

            return @$"{C_Autogenerated}

{KW_Using} {NS_Localization};
{KW_Using} {NS_Localization_Abstractions};

{KW_Namespace} {config.GenNamespace}
{{
    {KW_Public} {KW_Static} {KW_Class} {config.GenClass}
    {{
        {KW_Static} {config.GenClass}()
        {{
            {CL_TranslationProvider} = {KW_New} {CL_TranslationProvider}(
                {KW_TypeOf}({config.GenClass}).{nameof(Type.Assembly)},
                @""{config.TranslationPath}"",
                ""{config.DefaultLocale}""
            );

{string.Join(NewLine, translationUnits)}
        }}

        {KW_Public} {KW_Static} {I_TranslationProvider} {CL_TranslationProvider} {{ {KW_Get}; }}

{string.Join(NewLine, translationProperties)}
    }}
}}
";
        }
    }
}
