using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.DTOs;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.DTOs
{
    public record AddDeviceConnectionDTO(
        string Name,
        DeviceViewModel Device,
        ConnectionOption Connection,
        ProtocolOption Protocol,
        IAddConnectionDTO ConnectionInfo,
        IAddProtocolDTO ProtocolInfo
    );
}
