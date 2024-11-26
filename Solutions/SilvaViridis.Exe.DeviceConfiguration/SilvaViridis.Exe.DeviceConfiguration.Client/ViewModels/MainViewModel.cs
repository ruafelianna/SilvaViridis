using SilvaViridis.Components;
using SilvaViridis.Components.Menu;
using SilvaViridis.Components.Menu.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using System.Reactive;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel(AppInteractions appInteractions)
        {
            _viewSettingsViewModel = new(appInteractions);

            Menu = new MenuSector(1, [
                new MenuEndpoint(1, _viewSettingsViewModel),

                new MenuEndpoint(100, new OneActionViewModel(
                    Strings.Menu_Exit.ValueObservable,
                    async () => await appInteractions.Exit
                        .Handle(Unit.Default)
                )),
            ]);
        }

        public IMenuSector Menu { get; }

        private readonly ViewSettingsViewModel _viewSettingsViewModel;
    }
}
