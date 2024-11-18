using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Localization.Abstractions;
using System;
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

            ValueObservable = provider
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
                });

            _valueHelper = ValueObservable
                .ToProperty(this, o => o.Value);
        }

        public string Key { get; }

        public IObservable<string> ValueObservable { get; }

        [ObservableAsProperty]
        private string? _value;
    }
}
