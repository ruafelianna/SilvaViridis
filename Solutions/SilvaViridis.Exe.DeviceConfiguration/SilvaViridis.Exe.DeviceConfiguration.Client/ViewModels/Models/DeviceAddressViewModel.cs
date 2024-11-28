using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models.Enums;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Models
{
    public partial class DeviceAddressViewModel : ViewModelBase
    {
        public DeviceAddressViewModel(
            DevicePortViewModel parent,
            AvailableProtocols protocol,
            IProtocolInfo protocolInfo
        )
        {
            Parent = parent;
            Protocol = protocol;
            ProtocolInfo = protocolInfo;
        }

        public DevicePortViewModel Parent { get; }

        public AvailableProtocols Protocol { get; }

        public IProtocolInfo ProtocolInfo { get; }

        [Reactive]
        private bool _isChosen;

        [Reactive]
        private bool _isOnline;
    }
}
