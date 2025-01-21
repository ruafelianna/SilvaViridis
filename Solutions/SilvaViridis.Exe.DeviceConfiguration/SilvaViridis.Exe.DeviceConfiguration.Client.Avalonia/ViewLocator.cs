using Avalonia.Controls;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.ViewModels.Batches;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Batches;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Connections;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Devices;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Dialogs;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Protocols;
using SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia.Views.Settings;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Batches;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Dialogs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Settings;

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
            Register<SerialPortConnectionViewModel, SerialPortConnectionView>();
            Register<ModbusRTUProtocolViewModel, ModbusRTUProtocolView>();
            Register<AddDeviceConnectionViewModel, AddDeviceConnectionView>();
            Register<IAddSerialPortConnectionViewModel, AddSerialPortConnectionView>();
            Register<AddModbusRTUProtocolViewModel, AddModbusRTUProtocolView>();
        }

        public override ViewDefinition Locate(object viewModel)
            => LocateCustom<IAddSerialPortConnectionViewModel>(viewModel)
                ?? base.Locate(viewModel);

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

        private ViewDefinition? LocateCustom<T>(object viewModel)
        {
            if (
                viewModel is T
                && Registrations.TryGetValue(
                    typeof(T),
                    out var viewDefinition
                )
            )
            {
                return viewDefinition;
            }

            return null;
        }
    }
}
