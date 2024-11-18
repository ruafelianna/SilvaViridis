using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel(AppInteractions appInteractions)
        {
            _selectedLanguage = AvailableLanguages.en_US;

            this
                .WhenAnyValue(vm => vm.SelectedLanguage)
                .Select(
                    async lang => await appInteractions
                        .ChangeLanguage
                        .Handle(lang)
                )
                .Concat()
                .Subscribe();

            Languages = new Dictionary<AvailableLanguages, IObservable<string>>
                {
                    [AvailableLanguages.en_US]
                        = Strings.Lang_English.ValueObservable,
                    [AvailableLanguages.ru_RU]
                        = Strings.Lang_Russian.ValueObservable,
                }
                .ToFrozenDictionary();

            CmdExit = ReactiveCommand
                .CreateFromTask(
                    async () => await appInteractions
                        .Exit
                        .Handle(Unit.Default)
                );
        }

        [Reactive]
        private AvailableLanguages _selectedLanguage;

        public IReadOnlyDictionary<AvailableLanguages, IObservable<string>> Languages { get; }

        public ReactiveCommand<Unit, Unit> CmdExit { get; }
    }
}
