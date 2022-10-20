using TrialBalanceWebApp.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrialBalanceWebApp.Entities
{
    public class Account : BaseEntity
    {
        public int BankAccount { get; set; }

        public int ClassId { get; set; }

        public int OpeningBalanceId { get; set; }
        [ForeignKey(nameof(OpeningBalanceId))]
        public Balance OpeningBalance { get; set; }

        public int RevenueId { get; set; }
        [ForeignKey(nameof(RevenueId))]
        public Revenue Revenue { get; set; }

        [NotMapped]
        public Balance OutgoingBalance
        {
            get => new Balance()
            {
                Active = OpeningBalance.Active == 0 ? 0 : OpeningBalance.Active + Revenue.Debit - Revenue.Credit,
                Passive = OpeningBalance.Passive == 0 ? 0 : OpeningBalance.Passive - (Revenue.Debit - Revenue.Credit)
            };
        }
    }
}
