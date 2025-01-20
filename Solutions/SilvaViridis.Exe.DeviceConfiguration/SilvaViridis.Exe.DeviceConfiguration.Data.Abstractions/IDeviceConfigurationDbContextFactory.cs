using SilvaViridis.Data.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Data.Abstractions
{
    public interface IDeviceConfigurationDbContextFactory :
        IDbContextFactory<IDeviceConfigurationDbContext>
    {
    }
}
