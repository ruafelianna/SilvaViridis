using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public partial class DeviceAddressViewModel : ViewModelBase
    {
        public DeviceAddressViewModel(
            DevicePortViewModel parent,
            IProtocolInfo protocolInfo
        )
        {
            Parent = parent;
            ProtocolInfo = protocolInfo;
        }

        public DevicePortViewModel Parent { get; }

        public IProtocolInfo ProtocolInfo { get; }

        [Reactive]
        private bool _isChosen;

        [Reactive]
        private bool _isOnline;
    }
}
