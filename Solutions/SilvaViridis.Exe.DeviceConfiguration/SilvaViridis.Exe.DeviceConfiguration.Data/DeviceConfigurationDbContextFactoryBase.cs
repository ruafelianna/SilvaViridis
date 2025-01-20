using Microsoft.EntityFrameworkCore.Design;
using SilvaViridis.Data.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Data.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Data
{
    public abstract class DeviceConfigurationDbContextFactoryBase<TDbContext> :
        IDesignTimeDbContextFactory<TDbContext>,
        IDeviceConfigurationDbContextFactory
        where TDbContext : DeviceConfigurationDbContextBase
    {
        public abstract TDbContext CreateDbContext(string[] args);

        IDeviceConfigurationDbContext IDbContextFactory<IDeviceConfigurationDbContext>.CreateDbContext(string[] args)
            => CreateDbContext(args);
    }
}
