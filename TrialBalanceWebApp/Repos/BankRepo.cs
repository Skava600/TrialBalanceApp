using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using TrialBalanceWebApp.Data;
using TrialBalanceWebApp.Entities;
using TrialBalanceWebApp.Repos.Base;
using TrialBalanceWebApp.Repos.Interfaces;
using TrialBalanceWebApp.Services.Logging.Interfaces;

namespace TrialBalanceWebApp.Repos
{
    public class BankRepo : BaseRepo<Bank>, IBankRepo
    {
        public BankRepo(ApplicationDbContext context) : base(context)
        {
        }

        internal BankRepo(IAppLogging<ApplicationDbContext> logging, DbContextOptions<ApplicationDbContext> options) :
        base(logging, options)
        {
        }

        public override async Task<Bank?> Find(int? id)
            => await  Table
               .Include(b => b.AccountClasses)
               .ThenInclude(accountClass => accountClass.Accounts)
               .ThenInclude(account => account.Revenue)
               .Include(b => b.AccountClasses)
               .ThenInclude(ac => ac.Accounts)
               .ThenInclude(ac => ac.OpeningBalance)
               .FirstOrDefaultAsync(b => b.Id == id);
        public override IEnumerable<Bank> GetAll() 
            => Table
               .Include(b => b.AccountClasses)
               .ThenInclude(accountClass => accountClass.Accounts)
               .ThenInclude(account => account.Revenue)
               .Include(b => b.AccountClasses)
               .ThenInclude(ac => ac.Accounts)
               .ThenInclude(ac => ac.OpeningBalance)
               .OrderBy(b => b.Name);

       
    }
}
