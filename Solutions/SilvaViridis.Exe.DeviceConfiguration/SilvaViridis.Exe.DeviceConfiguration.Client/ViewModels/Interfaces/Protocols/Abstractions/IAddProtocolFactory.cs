namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions
{
    public interface IAddProtocolFactory
    {
        IAddProtocolViewModel CreateModbusProtocol();
    }
}
