using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;
using System;
using SilvaViridis.Common.Text.Extensions;
using SilvaViridis.Exe.DeviceConfiguration.Data.PostgreSQL.Assets.Translations;
using System.Globalization;

namespace SilvaViridis.Exe.DeviceConfiguration.Data.PostgreSQL
{
    public class PgDeviceConfigurationDbContextFactory :
        DeviceConfigurationDbContextFactoryBase<PgDeviceConfigurationDbContext>
    {
        public override PgDeviceConfigurationDbContext CreateDbContext(
            string[] args
        )
        {
            Strings.TranslationProvider.Culture = args.Length == 0
                || args[0].IsNullOrWhiteSpace()
                    ? CultureInfo.CurrentCulture
                    : new(args[0]);

            if (args.Length < 2)
            {
                throw new ArgumentException(
                    Strings.Error__Migration_Args__Count.Value,
                    nameof(args)
                );
            }

            var connStringBuilder = new NpgsqlConnectionStringBuilder(args[1]);

            var dbContextBuilder = new DbContextOptionsBuilder()
                .UseNpgsql(
                    connStringBuilder.ConnectionString,
                    x => x.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        connStringBuilder.SearchPath
                    )
                )
                .EnableSensitiveDataLogging();

            return new PgDeviceConfigurationDbContext(dbContextBuilder.Options);
        }
    }
}
