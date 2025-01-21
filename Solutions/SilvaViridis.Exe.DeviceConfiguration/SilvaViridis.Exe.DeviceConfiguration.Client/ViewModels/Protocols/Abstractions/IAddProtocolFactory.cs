namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Protocols.Abstractions
{
    public interface IAddProtocolFactory
    {
        IAddProtocolViewModel CreateModbusProtocol();
    }
}
