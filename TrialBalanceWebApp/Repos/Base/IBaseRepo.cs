using TrialBalanceWebApp.Data;
using TrialBalanceWebApp.Models.Base;

namespace TrialBalanceWebApp.Repos.Base
{
    public interface IBaseRepo<TEntity> where TEntity : BaseEntity
    {
        ApplicationDbContext Context { get; }
        void ExecuteQuery(string sql, object[] sqlParametersObjects);
        IEnumerable<TEntity> GetAll();
        Task<TEntity?> Find(int? id);
        Task<int> Add(TEntity entity, bool persist = true);
        Task<int> AddRange(IEnumerable<TEntity> entities, bool persist = true);
        Task<int> Update(TEntity entity, bool persist = true);
        Task<int> UpdateRange(IEnumerable<TEntity> entities, bool persist = true);
        Task<int> Delete(TEntity entity, bool persist = true);
        Task<int> DeleteRange(IEnumerable<TEntity> entities, bool persist = true);
        Task<int> SaveChanges();
    }
}
