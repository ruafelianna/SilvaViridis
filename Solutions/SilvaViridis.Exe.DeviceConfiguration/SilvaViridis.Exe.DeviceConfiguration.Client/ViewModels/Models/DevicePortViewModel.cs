using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Enums;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models
{
    public partial class DevicePortViewModel : ViewModelBase
    {
        public DevicePortViewModel(
            string name,
            DeviceViewModel device,
            AvailableConnections connection,
            IConnectionInfo connectionInfo
        )
        {
            Name = name;
            Device = device;
            Connection = connection;
            ConnectionInfo = connectionInfo;
        }

        public string Name { get; }

        public DeviceViewModel Device { get; }

        public AvailableConnections Connection { get; }

        public IConnectionInfo ConnectionInfo { get; }

        [Reactive]
        private bool _isChosen;

        [Reactive]
        private bool _isOnline;
    }
}
