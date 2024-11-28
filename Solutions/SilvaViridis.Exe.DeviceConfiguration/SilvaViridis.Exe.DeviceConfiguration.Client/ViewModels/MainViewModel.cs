using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Menu;
using SilvaViridis.Components.Menu.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Batches;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Settings;
using System.Reactive;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel(AppInteractions appInteractions)
        {
            _viewSettingsViewModel = new(appInteractions);

            _deviceConnectionsViewModel = new();

            Menu = CreateMenu(appInteractions);
        }

        public IMenuSector Menu { get; }

        [Reactive(SetModifier = AccessModifier.Private)]
        private ViewModelBase? _content;

        private readonly ViewSettingsViewModel _viewSettingsViewModel;

        private readonly DeviceConnectionsViewModel _deviceConnectionsViewModel;

        private IMenuItem? _disabledMenu;

        private MenuSector CreateMenu(AppInteractions appInteractions)
        {
            static void doNothing() { }

            #region Header

            var viewSettings = new MenuEndpoint(
                1,
                _viewSettingsViewModel
            );

            #endregion

            #region Configuration

            var configuration_Create = new MenuEndpoint(
                1,
                new OneActionViewModel(
                    Strings.Menu_Create,
                    doNothing
                )
            );

            var configuration_List = new MenuEndpoint(
                1,
                new OneActionViewModel(
                    Strings.Menu_List,
                    doNothing
                )
            );

            var configuration = new HeadedMenuSector(
                10,
                Strings.Menu_Configurations,
                [
                    configuration_Create,
                    configuration_List,
                ]
            );

            #endregion

            #region Batches

            MenuEndpoint batches_Create = null!;

            batches_Create = new MenuEndpoint(
                1,
                new OneActionViewModel(
                    Strings.Menu_Create,
                    () => ShowContent(
                        new CreateBatchViewModel(
                            async () => HideContent(),
                            async () => HideContent()
                        ),
                        batches_Create
                    )
                )
            );

            MenuEndpoint batches_List = null!;

            batches_List = new MenuEndpoint(
                2,
                new OneActionViewModel(
                    Strings.Menu_List,
                    () => ShowContent(
                        new BatchListViewModel(),
                        batches_List
                    )
                )
            );

            var batches = new HeadedMenuSector(
                20, Strings.Menu_Batches,
                [
                    batches_Create,
                    batches_List,
                ]
            );

            #endregion

            #region Devices

            MenuEndpoint devices_Connections = null!;

            devices_Connections = new MenuEndpoint(
                1,
                new OneActionViewModel(
                    Strings.Menu_Connections,
                    () => ShowContent(
                        _deviceConnectionsViewModel,
                        devices_Connections
                    )
                )
            );

            var devices_Polling = new MenuEndpoint(
                2,
                new OneActionViewModel(
                    Strings.Menu_Polling,
                    doNothing
                )
            );

            var devices = new HeadedMenuSector(
                30,
                Strings.Menu_Devices,
                [
                    devices_Connections,
                    devices_Polling,
                ]
            );

            #endregion

            #region Scripts

            var scripts = new MenuEndpoint(
                40,
                new OneActionViewModel(
                    Strings.Menu_Scripts,
                    doNothing
                )
            );

            #endregion

            #region Reports

            var reports = new MenuEndpoint(
                50,
                new OneActionViewModel(
                    Strings.Menu_Reports,
                    doNothing
                )
            );

            #endregion

            #region Footer

            var exit = new MenuEndpoint(
                1000,
                new OneActionViewModel(
                    Strings.Menu_Exit,
                    async () => await appInteractions.Exit
                        .Handle(Unit.Default)
                )
            );

            #endregion

            return new MenuSector(
                1,
                [
                    viewSettings,
                    configuration,
                    batches,
                    devices,
                    scripts,
                    reports,
                    exit,
                ]
            );
        }

        private void ShowContent(ViewModelBase vm, IMenuItem menu)
        {
            if (_disabledMenu is not null)
            {
                _disabledMenu.IsEnabled = true;
            }

            _disabledMenu = menu;
            _disabledMenu.IsEnabled = false;

            Content = vm;
        }

        private void HideContent()
        {
            Content = null;

            if (_disabledMenu is not null)
            {
                _disabledMenu.IsEnabled = true;
                _disabledMenu = null;
            }
        }
    }
}
