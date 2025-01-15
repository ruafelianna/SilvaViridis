using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Enums;
using System.Collections.Generic;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.DTOs
{
    public record ConnectionOption(
        AvailableConnections Connection,
        ITranslationUnit Name,
        IEnumerable<AvailableProtocols> SupportedProtocols
    );
}
