using HanumanInstitute.MvvmDialogs.Avalonia;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Dialogs;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Interfaces;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Dialogs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia
{
    public class ViewLocator : StrongViewLocator
    {
        public ViewLocator()
        {
            Register<MainViewModel, MainView, MainWindow>();
            Register<ViewSettingsViewModel, ViewSettingsView>();
            Register<DialogErrorViewModel, DialogErrorView>();
            Register<OneActionViewModel, OneActionView>();
            Register<CreateBatchViewModel, CreateBatchView>();
        }
    }
}
