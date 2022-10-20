using TrialBalanceWebApp.Models.Base;

namespace TrialBalanceWebApp.Entities
{
    public class Bank : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<AccountClass> AccountClasses { get; set; }
    }
}
