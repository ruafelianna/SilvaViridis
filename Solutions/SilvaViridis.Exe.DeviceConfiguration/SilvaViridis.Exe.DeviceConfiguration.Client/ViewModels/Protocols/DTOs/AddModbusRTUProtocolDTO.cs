using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.DTOs
{
    public record AddModbusRTUProtocolDTO(
        byte AddressesStartingWith,
        int AddressesQuantity,
        int AddressesStep
    ) : IAddProtocolDTO;
}
