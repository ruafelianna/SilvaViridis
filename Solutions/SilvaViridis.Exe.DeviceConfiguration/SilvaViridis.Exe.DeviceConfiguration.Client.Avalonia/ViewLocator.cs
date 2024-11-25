using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views;
using HanumanInstitute.MvvmDialogs.Avalonia;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia
{
    public class ViewLocator : StrongViewLocator
    {
        public ViewLocator()
        {
            Register<MainViewModel, MainView, MainWindow>();
            Register<ViewSettingsViewModel, ViewSettingsView>();
            Register<ExitViewModel, ExitView>();
        }
    }
}
