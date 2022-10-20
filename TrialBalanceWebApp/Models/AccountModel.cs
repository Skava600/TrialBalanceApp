using TrialBalanceWebApp.Entities;

namespace TrialBalanceWebApp.Models
{
    public class AccountModel
    {
        public string BankAccount { get; set; }
        public double OpeningActive { get; set; }
        public double OpeningPassive { get; set; }
        public double RevenueDebit { get; set; }
        public double RevenueCredit { get; set; }
        public double OutgoingActive { get; set; }
        public double OutgoingPassive { get; set; }
        public bool IsBold { get; set; }

        public AccountModel(string bankAccount, double openingActive, double openingPassive, double revenueDebit, double revenueCredit, double outgoingActive, double outgoingPassive, bool isBold)
        {
            BankAccount = bankAccount;
            OpeningActive = openingActive;
            OpeningPassive = openingPassive;
            RevenueDebit = revenueDebit;
            RevenueCredit = revenueCredit;
            OutgoingActive = outgoingActive;
            OutgoingPassive = outgoingPassive;
            IsBold = isBold;
        }

        public AccountModel(Account account, bool isBold = false)
        {
            BankAccount = account.BankAccount.ToString();
            OpeningActive = account.OpeningBalance.Active;
            OpeningPassive = account.OpeningBalance.Passive;
            RevenueDebit = account.Revenue.Debit;
            RevenueCredit = account.Revenue.Credit;
            OutgoingActive = account.OutgoingBalance.Active;
            OutgoingPassive = account.OutgoingBalance.Passive;
            IsBold = isBold;
        }
        public AccountModel()
        {

        }
    }
}
