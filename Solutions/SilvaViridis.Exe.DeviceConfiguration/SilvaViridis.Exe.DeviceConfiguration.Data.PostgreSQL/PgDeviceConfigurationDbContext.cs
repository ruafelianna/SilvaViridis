using Microsoft.EntityFrameworkCore;

namespace SilvaViridis.Exe.DeviceConfiguration.Data.PostgreSQL
{
    public class PgDeviceConfigurationDbContext : DeviceConfigurationDbContextBase
    {
        protected PgDeviceConfigurationDbContext() : base()
        {
        }

        public PgDeviceConfigurationDbContext(DbContextOptions options) :
            base(options)
        {
        }
    }
}
