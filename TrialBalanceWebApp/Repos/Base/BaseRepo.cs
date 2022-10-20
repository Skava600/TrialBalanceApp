using Microsoft.EntityFrameworkCore;
using TrialBalanceWebApp.Data;
using TrialBalanceWebApp.Models.Base;
using TrialBalanceWebApp.Services.DataServices.Dal.Exceptions;
using TrialBalanceWebApp.Services.Logging.Interfaces;

namespace TrialBalanceWebApp.Repos.Base
{
    public abstract class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity, new()
    {
        private readonly bool _disposeContext;
        public ApplicationDbContext Context { get; }
        public DbSet<T> Table { get; }
        protected BaseRepo(ApplicationDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
            _disposeContext = false;
        }
        protected BaseRepo(IAppLogging<ApplicationDbContext> logging, DbContextOptions<ApplicationDbContext> options)
        : this(new ApplicationDbContext(logging, options))
        {
            _disposeContext = true;
        }

        public virtual async Task<T?> Find(int? id) => await Table.FindAsync(id);

        public virtual IEnumerable<T> GetAll() => Table.AsQueryable();

        public virtual async Task<int> Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? await SaveChanges() : 0;
        }
        public virtual async Task<int> AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? await SaveChanges() : 0;
        }
        public virtual async Task<int> Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ?await  SaveChanges() : 0;
        }
        public virtual async Task<int> UpdateRange(IEnumerable<T> entities,
        bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? await SaveChanges() : 0;
        }
        public virtual async Task<int> Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? await SaveChanges() : 0;
        }
        public virtual async Task<int> DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? await SaveChanges() : 0;
        }

        public void ExecuteQuery(string sql, object[] sqlParametersObjects) => Context.Database.ExecuteSqlRaw(sql, sqlParametersObjects);


        public async Task<int> SaveChanges()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (CustomException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Подлежит регистрации в журнале и надлежащей обработке.
                throw new CustomException("An error occurred updating the database", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (
                _disposeContext)
                {
                    Context.Dispose();
                }
            }
            _isDisposed = true;
        }
        ~BaseRepo()
        {
            Dispose(false);
        }
    }
}
