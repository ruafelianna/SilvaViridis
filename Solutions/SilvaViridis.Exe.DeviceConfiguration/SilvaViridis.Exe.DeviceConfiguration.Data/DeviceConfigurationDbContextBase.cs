using Microsoft.EntityFrameworkCore;
using SilvaViridis.Data;
using SilvaViridis.Exe.DeviceConfiguration.Data.Abstractions;

namespace SilvaViridis.Exe.DeviceConfiguration.Data
{
    public class DeviceConfigurationDbContextBase :
        DbContextBase,
        IDeviceConfigurationDbContext
    {
        protected DeviceConfigurationDbContextBase() : base()
        {
        }

        public DeviceConfigurationDbContextBase(DbContextOptions options) :
            base(options)
        {
        }
    }
}
