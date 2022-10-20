using TrialBalanceWebApp.Models.Base;
using TrialBalanceWebApp.Repos.Base;
using TrialBalanceWebApp.Services.DataServices.Interfaces;
using TrialBalanceWebApp.Services.Logging.Interfaces;

namespace TrialBalanceWebApp.Services.DataServices.Dal.Base
{
    public abstract class DalDataServiceBase<TEntity, TDataService> : IDataServiceBase<TEntity>
        where TEntity : BaseEntity, new()
        where TDataService : class
    {
        protected readonly IBaseRepo<TEntity> MainRepo;
        protected readonly IAppLogging<TDataService> AppLoggingInstance;


        protected DalDataServiceBase(IAppLogging<TDataService> logging, IBaseRepo<TEntity> mainRepo)
        {
            MainRepo = mainRepo;
            AppLoggingInstance = logging;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => MainRepo.GetAll();

        public async Task<TEntity?> FindAsync(int? id) => await MainRepo.Find(id);

        public async Task<TEntity> UpdateAsync(TEntity entity, bool persist = true)
        {
            await MainRepo.Update(entity, persist);   
            return entity;
        }

        public async Task DeleteAsync(TEntity entity, bool persist = true)
            => await MainRepo.Delete(entity, persist);

        public async Task<TEntity> AddAsync(TEntity entity, bool persist = true)
        {
            await MainRepo.Add(entity, persist);
            return entity;
        }
    }
}
