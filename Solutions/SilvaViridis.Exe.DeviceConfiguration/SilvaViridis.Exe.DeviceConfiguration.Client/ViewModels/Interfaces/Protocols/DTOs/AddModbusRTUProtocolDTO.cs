using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.DTOs
{
    public record AddModbusRTUProtocolDTO(
        byte AddressesStartingWith,
        int AddressesQuantity,
        int AddressesStep
    ) : IAddProtocolDTO;
}
