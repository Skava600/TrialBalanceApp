using TrialBalanceWebApp.Entities;
using TrialBalanceWebApp.Repos;
using TrialBalanceWebApp.Repos.Base;
using TrialBalanceWebApp.Repos.Interfaces;
using TrialBalanceWebApp.Services.DataServices.Dal.Base;
using TrialBalanceWebApp.Services.Logging.Interfaces;

namespace TrialBalanceWebApp.Services.DataServices.Dal
{
    public class BankDalDataService : DalDataServiceBase<Bank, BankDalDataService>
    {
        private readonly IBankRepo _repo;

        public BankDalDataService(IAppLogging<BankDalDataService> logging, IBankRepo repo) : base(logging, repo)
        {
            _repo = repo;
        }
    }
}
