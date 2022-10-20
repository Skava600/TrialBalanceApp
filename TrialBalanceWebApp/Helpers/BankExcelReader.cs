using IronXL;
using TrialBalanceWebApp.Models;

namespace TrialBalanceWebApp.Helpers
{
    public static class BankExcelReader
    {
        public static Bank ReadExcelDoc(WorkSheet worksheet)
        {
            Bank bank = new Bank()
            {
                Name = Path.GetFileName(worksheet.Name),
                AccountClasses = new List<AccountClass>()
            };

            AccountClass? currentAccountClass = null;

            Account currentAccount;
            for (int i = 9; i < worksheet.RowCount - 1; i++)
            {
                var range = worksheet[$"A{i}:E{i}"].ToList();
                int bankAccount;
                if (Int32.TryParse(range[0].Text, out bankAccount) && currentAccountClass != null)
                {
                    if (bankAccount / 100 == 0) 
                        continue;

                    Balance openingBalance = new Balance()
                    {
                        Active = (double)range[1].Value,
                        Passive = (double)range[2].Value,
                    };
                    Revenue revenue = new Revenue()
                    {
                        Debit = (double)range[3].Value,
                        Credit = (double)range[4].Value,
                    };

                    currentAccount = new Account()
                    {
                        BankAccount = bankAccount,
                        OpeningBalance = openingBalance,
                        Revenue = revenue,
                    };

                    currentAccountClass.Accounts.Add(currentAccount);
                }
                else
                {
                    if (currentAccountClass != null && currentAccountClass.Accounts.Any())
                    {
                        bank.AccountClasses.Add(currentAccountClass);
                    }
                    currentAccountClass = new AccountClass()
                    {
                        Accounts = new List<Account>(),
                        Name = range[0].Text,
                    };

                }

            }

            return bank;
        }
    }
}
