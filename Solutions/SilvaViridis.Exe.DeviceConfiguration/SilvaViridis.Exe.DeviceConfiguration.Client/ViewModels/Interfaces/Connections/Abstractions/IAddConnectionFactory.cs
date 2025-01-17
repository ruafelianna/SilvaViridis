namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Abstractions
{
    public interface IAddConnectionFactory
    {
        IAddConnectionViewModel CreateSerialPortConnection();
    }
}
