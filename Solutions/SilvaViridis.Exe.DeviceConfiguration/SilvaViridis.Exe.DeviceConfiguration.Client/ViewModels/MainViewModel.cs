using SilvaViridis.Components;
using SilvaViridis.Components.Menu;
using SilvaViridis.Components.Menu.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel(AppInteractions appInteractions)
        {
            _viewSettingsViewModel = new(appInteractions);
            _exitViewModel = new(appInteractions);

            Menu = new MenuSector(1, [
                new MenuEndpoint(1, _viewSettingsViewModel),
                new MenuEndpoint(100, _exitViewModel),
            ]);
        }

        public IMenuSector Menu { get; }

        private readonly ViewSettingsViewModel _viewSettingsViewModel;

        private readonly ExitViewModel _exitViewModel;
    }
}
