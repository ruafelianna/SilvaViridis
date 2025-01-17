using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.DTOs
{
    public record AddSerialPortConnectionDTO(
        string PortName
    ) :
        IAddConnectionDTO,
        IAddSerialPortConnectionDTO;
}
