using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Menu;
using SilvaViridis.Components.Menu.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces;
using System.Reactive;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel(AppInteractions appInteractions)
        {
            _viewSettingsViewModel = new(appInteractions);

            static void doNothing() { }

            Menu = new MenuSector(1, [
                new MenuEndpoint(1, _viewSettingsViewModel),

                new HeadedMenuSector(10, Strings.Menu_Configurations.ValueObservable, [
                    new MenuEndpoint(1, new OneActionViewModel(
                        Strings.Menu_Create.ValueObservable,
                        doNothing
                    )),
                    new MenuEndpoint(2, new OneActionViewModel(
                        Strings.Menu_List.ValueObservable,
                        doNothing
                    )),
                ]),

                new HeadedMenuSector(20, Strings.Menu_Batches.ValueObservable, [
                    new MenuEndpoint(1, new OneActionViewModel(
                        Strings.Menu_Create.ValueObservable,
                        () => Content = new CreateBatchViewModel(
                            async () => Content = null,
                            async () => Content = null
                        )
                    )),
                    new MenuEndpoint(2, new OneActionViewModel(
                        Strings.Menu_List.ValueObservable,
                        () => Content = new BatchListViewModel()
                    )),
                ]),

                new MenuEndpoint(30, new OneActionViewModel(
                    Strings.Menu_Devices.ValueObservable,
                    doNothing
                )),

                new MenuEndpoint(40, new OneActionViewModel(
                    Strings.Menu_Pollings.ValueObservable,
                    doNothing
                )),

                new MenuEndpoint(50, new OneActionViewModel(
                    Strings.Menu_Scripts.ValueObservable,
                    doNothing
                )),

                new MenuEndpoint(60, new OneActionViewModel(
                    Strings.Menu_Reports.ValueObservable,
                    doNothing
                )),

                new MenuEndpoint(100, new OneActionViewModel(
                    Strings.Menu_Exit.ValueObservable,
                    async () => await appInteractions.Exit
                        .Handle(Unit.Default)
                )),
            ]);
        }

        public IMenuSector Menu { get; }

        [Reactive]
        private ViewModelBase? _content;

        private readonly ViewSettingsViewModel _viewSettingsViewModel;
    }
}
