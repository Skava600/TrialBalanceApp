using System.ComponentModel.DataAnnotations;
using TrialBalanceWebApp.Models.Base;

namespace TrialBalanceWebApp.Entities
{
    public class Balance : BaseEntity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }    
        public double Active { get; set; }
        public double Passive { get; set; }

    }
}
