using ReactiveUI.Validation.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions
{
    public interface IAddConnectionViewModel : IValidatableViewModel
    {
        IAddConnectionDTO AsDTO();
    }
}
