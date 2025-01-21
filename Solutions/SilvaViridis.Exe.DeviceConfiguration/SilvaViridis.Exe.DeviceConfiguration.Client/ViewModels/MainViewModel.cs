using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Menu;
using SilvaViridis.Components.Menu.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Batches;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Settings;
using System;
using System.Collections.Concurrent;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            AppInteractions appInteractions,
            IAddConnectionFactory addConnectionFactory,
            IAddProtocolFactory addProtocolFactory
        )
        {
            ViewSettings = new(appInteractions);

            var devs = new SourceCache<DeviceViewModel, string>(x => x.Name);

            devs
                .Connect()
                .SortBy(x => x.Name)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out var list)
                .Subscribe();

            Task refreshDevices() => Task.Run(() =>
            {
                using var suspend = devs.SuspendNotifications();
                devs.Clear();
                for (int i = 0; i < 5; i++)
                {
                    devs.AddOrUpdate(
                        new DeviceViewModel(
                            Guid.NewGuid().ToString()
                        )
                    );
                }
            });

            _deviceConnectionsViewModel = new(
                ShowContent,
                HideContent,
                list,
                refreshDevices,
                addConnectionFactory,
                addProtocolFactory
            );

            Menu = CreateMenu(appInteractions);

            IsMenuEnabled = true;
        }

        public ViewSettingsViewModel ViewSettings { get; }

        public IMenuSector Menu { get; }

        [Reactive]
        private bool _isMenuEnabled;

        [Reactive(SetModifier = AccessModifier.Private)]
        private ViewModelBase? _content;

        private readonly DeviceConnectionsViewModel _deviceConnectionsViewModel;

        private readonly object _menuLock = new();

        private readonly ConcurrentStack<ViewModelBase> _menuStack = new();

        private MenuSector CreateMenu(AppInteractions appInteractions)
        {
            static void doNothing() { }

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
                        )
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
                    configuration,
                    batches,
                    devices,
                    scripts,
                    reports,
                    exit,
                ]
            );
        }

        private void ShowContent(ViewModelBase vm) => ShowContent(vm, null);

        private void ShowContent(
            ViewModelBase vm,
            IMenuItem? menuItem
        )
        {
            lock (_menuLock)
            {
                if (menuItem is null)
                {
                    IsMenuEnabled = false;
                }
                else
                {
                    menuItem.IsEnabled = false;
                    _menuStack.Clear();
                }

                if (Content is not null)
                {
                    _menuStack.Push(Content);
                }

                Content = vm;
            }
        }

        private void HideContent() => HideContent(null);

        private void HideContent(IMenuItem? menuItem)
        {
            lock (_menuLock)
            {
                if (_menuStack.TryPop(out var vm))
                {
                    Content = vm;
                }
                else
                {
                    Content = null;
                }

                if (menuItem is null)
                {
                    IsMenuEnabled = true;
                }
                else
                {
                    menuItem.IsEnabled = true;
                }
            }
        }
    }
}
