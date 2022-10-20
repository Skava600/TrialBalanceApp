using System.ComponentModel.DataAnnotations;
using TrialBalanceWebApp.Models.Base;

namespace TrialBalanceWebApp.Entities
{
    public class Revenue : BaseEntity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
