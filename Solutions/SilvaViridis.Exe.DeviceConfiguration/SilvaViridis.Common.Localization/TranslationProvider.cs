using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Localization.Abstractions;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using Tommy;

namespace SilvaViridis.Common.Localization
{
    public partial class TranslationProvider :
        ReactiveObject,
        ITranslationProvider
    {
        public TranslationProvider(
            Assembly assembly,
            string path,
            string invariant
        )
        {
            Assembly = assembly;
            Path = path.Replace('\\', '.').Replace('/', '.');

            DefaultCulture = new(invariant);
            DefaultTranslation = GetTranslation(DefaultCulture);

            Culture = DefaultCulture;

            _translationHelper = this
                .WhenAnyValue(o => o.Culture)
                .Select(culture =>
                {
                    if (
                        culture.IsNeutralCulture
                        || culture.Name == DefaultCulture.Name
                    )
                    {
                        return DefaultTranslation;
                    }

                    return GetTranslation(culture);
                })
                .ToProperty(this, o => o.Translation);
        }

        [Reactive]
        private CultureInfo _culture = null!;

        public CultureInfo DefaultCulture { get; }

        public string Path { get; }

        public Assembly Assembly { get; }

        [ObservableAsProperty]
        private IDictionary<string, string?>? _translation;

        public IDictionary<string, string?>? DefaultTranslation { get; }

        private FrozenDictionary<string, string?>? GetTranslation(
            CultureInfo culture
        )
        {
            var respath = Assembly
                .GetManifestResourceNames()
                .FirstOrDefault(resName =>
                    resName == $"{Assembly.GetName().Name}{Path}.{culture.Name}.toml"
                );

            if (respath is null)
            {
                return null;
            }

            using var stream = Assembly.GetManifestResourceStream(respath);

            if (stream is null)
            {
                return null;
            }

            using var reader = new StreamReader(stream);

            return TOML.Parse(reader).AsTable.RawTable
                .Select(pair => new KeyValuePair<string, string?>(
                    pair.Key,
                    pair.Value
                ))
                .ToFrozenDictionary();
        }
    }
}
