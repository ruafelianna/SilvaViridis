using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.DTOs
{
    public record AddSerialPortConnectionDTO(
        string PortName
    ) :
        IAddConnectionDTO,
        IAddSerialPortConnectionDTO;
}
