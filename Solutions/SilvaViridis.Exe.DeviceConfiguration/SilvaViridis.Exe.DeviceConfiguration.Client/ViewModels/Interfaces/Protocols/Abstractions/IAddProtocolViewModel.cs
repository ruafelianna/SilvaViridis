using ReactiveUI.Validation.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions
{
    public interface IAddProtocolViewModel : IValidatableViewModel
    {
        IAddProtocolDTO AsDTO();
    }
}
