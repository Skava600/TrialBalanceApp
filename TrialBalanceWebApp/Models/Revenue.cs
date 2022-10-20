using System.ComponentModel.DataAnnotations;
using TrialBalanceWebApp.Models.Base;

namespace TrialBalanceWebApp.Models
{
    public class Revenue : BaseEntity
    {
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
