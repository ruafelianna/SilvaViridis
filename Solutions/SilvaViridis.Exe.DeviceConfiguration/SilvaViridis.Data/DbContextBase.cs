using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SilvaViridis.Data.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Data
{
    public class DbContextBase : DbContext, IDbContext
    {
        protected DbContextBase() : base()
        {
        }

        public DbContextBase(DbContextOptions options) : base(options)
        {
        }

        public IDbContextTransaction BeginTransaction()
            => Database.BeginTransaction();

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => Database.BeginTransactionAsync(cancellationToken);

        public bool CanConnect()
            => Database.CanConnect();

        public Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
            => Database.CanConnectAsync(cancellationToken);

        public void CommitTransaction()
            => Database.CommitTransaction();

        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
            => Database.CommitTransactionAsync(cancellationToken);

        public void RollbackTransaction()
            => Database.RollbackTransaction();

        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
            => Database.RollbackTransactionAsync(cancellationToken);

        public IQueryable<TResult> SqlQuery<TResult>(FormattableString sql)
            => Database.SqlQuery<TResult>(sql);

        public IQueryable<TResult> SqlQueryRaw<TResult>(string sql, params object[] parameters)
            => Database.SqlQueryRaw<TResult>(sql, parameters);
    }
}
