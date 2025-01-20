using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Data.Abstractions
{
    public interface IDbContext : IDisposable, IAsyncDisposable
    {
        DbContextId ContextId { get; }

        EntityEntry Add(object entity);

        EntityEntry<TEntity> Add<TEntity>(TEntity entity)
            where TEntity : class;

        ValueTask<EntityEntry> AddAsync(object entity,
            CancellationToken cancellationToken = default);

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        void AddRange(IEnumerable<object> entities);

        void AddRange(params object[] entities);

        Task AddRangeAsync(IEnumerable<object> entities,
            CancellationToken cancellationToken = default);

        Task AddRangeAsync(params object[] entities);

        EntityEntry Attach(object entity);

        EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
            where TEntity : class;

        void AttachRange(IEnumerable<object> entities);

        void AttachRange(params object[] entities);

        EntityEntry Entry(object entity);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        object? Find(Type entityType, params object?[]? keyValues);

        TEntity? Find<TEntity>(params object?[]? keyValues)
            where TEntity : class;

        ValueTask<object?> FindAsync(Type entityType,
            params object?[]? keyValues);

        ValueTask<object?> FindAsync(Type entityType,
            object?[]? keyValues, CancellationToken cancellationToken);

        ValueTask<TEntity?> FindAsync<TEntity>(
            params object?[]? keyValues)
            where TEntity : class;

        ValueTask<TEntity?> FindAsync<TEntity>(object?[]? keyValues,
            CancellationToken cancellationToken)
            where TEntity : class;

        IQueryable<TResult> FromExpression<TResult>(
            Expression<Func<IQueryable<TResult>>> expression);

        EntityEntry Remove(object entity);

        EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
            where TEntity : class;

        void RemoveRange(IEnumerable<object> entities);

        void RemoveRange(params object[] entities);

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken token = default);

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbSet<TEntity> Set<TEntity>(string name)
            where TEntity : class;

        EntityEntry Update(object entity);

        EntityEntry<TEntity> Update<TEntity>(TEntity entity)
            where TEntity : class;

        void UpdateRange(IEnumerable<object> entities);

        void UpdateRange(params object[] entities);

        //---------------------------------------------------

        IDbContextTransaction BeginTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default);

        bool CanConnect();

        Task<bool> CanConnectAsync(
            CancellationToken cancellationToken = default);

        void CommitTransaction();

        Task CommitTransactionAsync(
            CancellationToken cancellationToken = default);

        void RollbackTransaction();

        Task RollbackTransactionAsync(
            CancellationToken cancellationToken = default);

        IQueryable<TResult> SqlQuery<TResult>(
            FormattableString sql);

        IQueryable<TResult> SqlQueryRaw<TResult>(
            string sql, params object[] parameters);
    }
}
