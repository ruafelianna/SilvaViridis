namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Connections.Abstractions
{
    public interface IAddConnectionFactory
    {
        IAddConnectionViewModel CreateSerialPortConnection();
    }
}
