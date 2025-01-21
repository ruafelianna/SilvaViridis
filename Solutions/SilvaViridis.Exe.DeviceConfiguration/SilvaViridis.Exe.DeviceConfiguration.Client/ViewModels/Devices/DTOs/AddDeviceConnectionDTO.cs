using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.DTOs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.DTOs;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Devices.DTOs
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
