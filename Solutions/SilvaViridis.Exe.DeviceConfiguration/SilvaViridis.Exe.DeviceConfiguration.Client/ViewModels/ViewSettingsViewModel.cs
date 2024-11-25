using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public partial class ViewSettingsViewModel : ViewModelBase
    {
        public ViewSettingsViewModel(AppInteractions appInteractions)
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

            CmdChangeTheme = ReactiveCommand
                .CreateFromTask<AvailableThemes, Unit>(
                    async theme => await appInteractions
                        .ChangeTheme
                        .Handle(theme)
                );
        }

        [Reactive]
        private AvailableLanguages _selectedLanguage;

        public IReadOnlyDictionary<AvailableLanguages, IObservable<string>> Languages { get; }

        public ReactiveCommand<AvailableThemes, Unit> CmdChangeTheme { get; }
    }
}
