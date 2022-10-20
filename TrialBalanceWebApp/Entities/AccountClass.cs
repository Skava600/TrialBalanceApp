using System.ComponentModel.DataAnnotations;
using TrialBalanceWebApp.Models.Base;

namespace TrialBalanceWebApp.Entities
{
    public class AccountClass : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Account> Accounts { get; set; }

        public int BankId { get; set; }
    }
}
