using Avalonia.Controls;
using HanumanInstitute.MvvmDialogs.Avalonia;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.ViewModels.Interfaces;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Dialogs;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Interfaces;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Dialogs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models;

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
            Register<BatchListViewModelProxy, BatchListView>();
            Register<DeviceConnectionsViewModel, DeviceConnectionsView>();
            Register<SerialPortViewModel, SerialPortView>();
            Register<ModbusRTUViewModel, ModbusRTUView>();
        }

        public override Control Build(object? data)
        {
            bool setDataContext = true;

            if (data is BatchListViewModel blvm)
            {
                data = new BatchListViewModelProxy(blvm);
            }
            else
            {
                setDataContext = false;
            }

            var result = base.Build(data);

            if (setDataContext)
            {
                result.DataContext = data;
            }

            return result;
        }
    }
}
