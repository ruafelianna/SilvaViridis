using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Enums;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.DTOs
{
    public record ProtocolOption(
        AvailableProtocols Protocol,
        ITranslationUnit Name
    );
}
