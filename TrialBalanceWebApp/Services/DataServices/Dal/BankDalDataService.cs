using TrialBalanceWebApp.Models;
using TrialBalanceWebApp.Repos;
using TrialBalanceWebApp.Repos.Base;
using TrialBalanceWebApp.Services.DataServices.Dal.Base;
using TrialBalanceWebApp.Services.Logging.Interfaces;

namespace TrialBalanceWebApp.Services.DataServices.Dal
{
    public class BankDalDataService : DalDataServiceBase<Bank, BankDalDataService>
    {
        private readonly IBaseRepo<Bank> _repo;

        public BankDalDataService(IAppLogging<BankDalDataService> logging, IBaseRepo<Bank> repo) : base(logging, repo)
        {
            _repo = repo;
        }

    }
}
