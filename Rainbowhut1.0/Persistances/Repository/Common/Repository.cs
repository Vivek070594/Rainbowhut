using Rainbowhut1._0.Persistances.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rainbowhut1._0.Persistances.Repositories.Common
{
    public class Repository<TEntity, TEntityId> : Repository<TEntity>,
        IRepository<TEntity, TEntityId>
        where TEntity : class, ICorrelatedBy<TEntityId>, new()
    {
        public Repository(
            DbContext context)
            : base(context)
        {
        }

        public virtual async Task<TEntity> GetByIdAsync(
            TEntityId id,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return await GetQueryable()
                .ApplyIncludes(includes)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public virtual void CommitTransaction()
        {
            Context.Database.CommitTransaction();
        }

        public virtual void RollBackTransaction()
        {
            Context.Database.RollbackTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return Context.Database.BeginTransactionAsync();
        }

        public void MarkDeleted<TSubEntity>(
            TSubEntity entity)
            where TSubEntity : class
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }

        public void MarkUnchanged(
            TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Unchanged;
        }

        public void MarkUnchanged<TSubEntity>(
            TSubEntity entity)
            where TSubEntity : class
        {
            Context.Entry(entity).State = EntityState.Unchanged;
        }

        public void MarkModified(
            TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void MarkModified<TSubEntity>(
            TSubEntity entity)
            where TSubEntity : class
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }

    /// <summary>
    /// Repository that works with an entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        public Repository(DbContext context)
        {
            Context = context;
        }

        protected virtual DbContext Context { get; private set; }

        protected virtual IQueryable<TEntity> GetQueryable()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }

        public virtual async Task<List<TEntity>> GetItemsAsync<TResource>(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            ListFilter pagingFilter = null)
        {
            return await GetQueryable()
                .ApplyFilters(filters)
                .ApplyIncludes(includes)
                .ApplySorting<TEntity, TResource>(pagingFilter)
                .ApplyPaging(pagingFilter)
                .ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetItemsAsync(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            ListFilter pagingFilter = null)
        {
            return await GetQueryable()
                .ApplyFilters(filters)
                .ApplyIncludes(includes)
                .ApplyPaging(pagingFilter)
                .ToListAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return await GetQueryable()
                .ApplyFilters(filters)
                .ApplyIncludes(includes)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> LastOrDefaultAsync(
            Expression<Func<TEntity, bool>>[] filters = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            Expression<Func<TEntity, object>> orderByDescendingColumnSelector = null)
        {
            if (orderByDescendingColumnSelector == null)
            {
                orderByDescendingColumnSelector = e => e.GetPropValue("Id");
            }

            return await GetQueryable()
                .ApplyFilters(filters)
                .ApplyIncludes(includes)
                .OrderByDescending(orderByDescendingColumnSelector)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<int> CountAsync(
            params Expression<Func<TEntity, bool>>[] filters)
        {
            return await GetQueryable()
                .ApplyFilters(filters)
                .CountAsync();
        }

        public virtual async Task<bool> ExistsAsync(
            params Expression<Func<TEntity, bool>>[] filters)
        {
            return await GetQueryable()
                .ApplyFilters(filters)
                .AnyAsync();
        }

        public virtual async Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>>[] filters,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            return await GetQueryable()
                .ApplyIncludes(includes)
                .ApplyFilters(filters)
                .AnyAsync();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
        public async Task<int> SaveAsyncWithReturn()
        {
            int id=await Context.SaveChangesAsync();
            return id;
        }

        public void Update(TEntity entity)
        {

            Context.Set<TEntity>().Update(entity);
        }

        public void Add(TEntity entity)
        {

            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(
            TEntity entity)
        {
                Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(
            Func<TEntity, bool> predicate)
        {
            Context.Set<TEntity>()
                .RemoveRange
                (Context.Set<TEntity>()
                    .Where(predicate));
        }
    }
}
