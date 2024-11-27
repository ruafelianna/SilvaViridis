using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Localization.Abstractions;
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
            _selectedLanguage = AvailableLanguages.ru_RU;

            this
                .WhenAnyValue(vm => vm.SelectedLanguage)
                .Select(
                    async lang => await appInteractions
                        .ChangeLanguage
                        .Handle(lang)
                )
                .Concat()
                .Subscribe();

            Languages = new Dictionary<AvailableLanguages, ITranslationUnit>
                {
                    [AvailableLanguages.en_US]
                        = Strings.Lang_English,
                    [AvailableLanguages.ru_RU]
                        = Strings.Lang_Russian,
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

        public IReadOnlyDictionary<AvailableLanguages, ITranslationUnit> Languages { get; }

        public ReactiveCommand<AvailableThemes, Unit> CmdChangeTheme { get; }
    }
}
