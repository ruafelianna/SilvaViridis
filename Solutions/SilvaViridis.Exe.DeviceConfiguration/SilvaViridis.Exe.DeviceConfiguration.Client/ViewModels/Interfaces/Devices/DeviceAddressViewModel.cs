using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public partial class DeviceAddressViewModel : ViewModelBase
    {
        public DeviceAddressViewModel(
            DeviceConnectionViewModel parent,
            IProtocolViewModel protocolInfo
        )
        {
            Parent = parent;
            ProtocolInfo = protocolInfo;
        }

        public DeviceConnectionViewModel Parent { get; }

        public IProtocolViewModel ProtocolInfo { get; }

        [Reactive]
        private bool _isChosen;

        [Reactive]
        private bool _isOnline;
    }
}
