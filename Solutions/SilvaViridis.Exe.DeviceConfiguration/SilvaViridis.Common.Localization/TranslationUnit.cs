using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Localization.Abstractions;
using System.Reactive.Linq;

namespace SilvaViridis.Common.Localization
{
    public partial class TranslationUnit :
        ReactiveObject,
        ITranslationUnit
    {
        public TranslationUnit(
            ITranslationProvider provider,
            string key
        )
        {
            Key = key;

            _valueHelper = provider
                .WhenAnyValue(o => o.Translation)
                .Select(dict => {
                    string? value = null;

                    dict?.TryGetValue(Key, out value);

                    if (value is null)
                    {
                        provider.DefaultTranslation?.TryGetValue(
                            Key,
                            out value
                        );

                        if (value is null)
                        {
                            return Key;
                        }
                    }

                    return value;
                })
                .ToProperty(this, o => o.Value);
        }

        public string Key { get; }

        [ObservableAsProperty]
        private string? _value;
    }
}
