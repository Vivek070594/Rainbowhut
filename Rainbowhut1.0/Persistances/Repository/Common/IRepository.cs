using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rainbowhut1._0.Persistances.Filters;

namespace Rainbowhut1._0.Persistances.Repositories.Common
{
    public interface IRepository<TEntity, in TEntityId>
        where TEntity:class, ICorrelatedBy<TEntityId>, new()
    {
        Task<List<TEntity>> GetItemsAsync(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            ListFilter pagingFilter = null);

        Task<List<TEntity>> GetItemsAsync<TResource>(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            ListFilter pagingFilter = null);

        Task<TEntity> GetByIdAsync(
            TEntityId id,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> LastOrDefaultAsync(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            Expression<Func<TEntity, object>> orderByDescendingColumnSelector = null);

        Task<int> CountAsync(
            params Expression<Func<TEntity, bool>>[] filters);

        Task<bool> ExistsAsync(
            params Expression<Func<TEntity, bool>>[] filters);

        Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>>[] filters,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes);

        Task SaveAsync();

        void Delete(
            TEntity entity);

        void Update(
            TEntity entity
           );

        void Add(
            TEntity entity
           );

        IDbContextTransaction BeginTransaction();

        void CommitTransaction();

        void RollBackTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync();

        void MarkUnchanged(
            TEntity entity);

        void MarkModified(
            TEntity entity);

        void MarkModified<TSubEntity>(
            TSubEntity entity)
            where TSubEntity : class;

        void MarkUnchanged<TSubEntity>(
            TSubEntity entity)
            where TSubEntity : class;

        void MarkDeleted<TSubEntity>(
            TSubEntity entity)
            where TSubEntity : class;
    }

    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        Task<List<TEntity>> GetItemsAsync(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            ListFilter pagingFilter = null);

        Task<List<TEntity>> GetItemsAsync<TResource>(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            ListFilter pagingFilter = null);

        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<int> CountAsync(
            params Expression<Func<TEntity, bool>>[] filters);

        Task<bool> ExistsAsync(params Expression<Func<TEntity, bool>>[] filters);

        Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>>[] filters,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes);

        Task SaveAsync();
        Task<int> SaveAsyncWithReturn();

        void Delete(
            TEntity entity);

        void Update(
            TEntity entity
           );

        void Add(
            TEntity entity
           );

        void RemoveRange(
            Func<TEntity, bool> predicate);
    }
}
