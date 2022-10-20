using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TrialBalanceWebApp.Data;
using TrialBalanceWebApp.Entities;
using TrialBalanceWebApp.Models;
using TrialBalanceWebApp.Models.ViewModels;
using TrialBalanceWebApp.Services.DataServices.Dal;
using TrialBalanceWebApp.Services.DataServices.Interfaces;
using TrialBalanceWebApp.Services.Logging.Interfaces;

namespace TrialBalanceWebApp.Controllers
{
    public class BanksController : Controller
    {
        private readonly IDataServiceBase<Bank> _dataService;
        private IAppLogging<BanksController> _logger;

        public BanksController(IAppLogging<BanksController> appLogging, IDataServiceBase<Bank> dataService)
        {
            _dataService = dataService;
            _logger = appLogging;
        }

        // GET: Banks
        public async Task<IActionResult> Index()
        {
              return View(await _dataService.GetAllAsync());
        }

        // GET: Banks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _dataService.FindAsync(id);

            if (bank == null)
            {
                return NotFound();
            }

            BankViewModel bankViewModel = new BankViewModel()
            {
                Name = bank.Name,
            };

            List<AccountModel> accountsInClass;
            bankViewModel.Classes = bank.AccountClasses.Select(accountClass =>
            {
                var groups = accountClass.Accounts.GroupBy(a => a.BankAccount / 100);
                accountsInClass = new List<AccountModel>();
                foreach (var group in groups)
                {
                    foreach (var account in group)
                        accountsInClass.Add(new AccountModel(account));

                    //Добавление аккаунта по группе с б/сч из двух цифр
                    accountsInClass.Add(new AccountModel(group.Key.ToString(),
                                group.Sum(a => a.OpeningBalance.Active), group.Sum(a => a.OpeningBalance.Passive),
                                group.Sum(a => a.Revenue.Debit), group.Sum(a => a.Revenue.Credit),
                                group.Sum(a => a.OutgoingBalance.Active), group.Sum(a => a.OutgoingBalance.Active), true));
                }

                // Добавление аккаунта по классу
                accountsInClass.Add(new AccountModel("По классу",
                            accountClass.Accounts.Sum(a => a.OpeningBalance.Active), accountClass.Accounts.Sum(a => a.OpeningBalance.Passive),
                            accountClass.Accounts.Sum(a => a.Revenue.Debit), accountClass.Accounts.Sum(a => a.Revenue.Credit),
                            accountClass.Accounts.Sum(a => a.OutgoingBalance.Active), accountClass.Accounts.Sum(a => a.OutgoingBalance.Active), true));

                return new ClassModel()
                {
                    Name = accountClass.Name,
                    Accounts = accountsInClass,
                };
            });


            // Добавление общего аккаунта
            var classAccounts = bankViewModel.Classes.Select(cl => cl.Accounts.Where(ac => ac.BankAccount.Equals("По классу"))).SelectMany(ac => ac);
            bankViewModel.TotalAccount = new AccountModel("Баланс",
                classAccounts.Sum(cl => cl.OpeningActive), classAccounts.Sum(cl => cl.OpeningPassive),
                classAccounts.Sum(cl => cl.RevenueDebit), classAccounts.Sum(cl => cl.RevenueCredit),
                classAccounts.Sum(cl => cl.OutgoingActive), classAccounts.Sum(cl => cl.OutgoingPassive), true);
            return View(bankViewModel);
        }

        [HttpGet("download")]
        public async Task<IActionResult> Export(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }    

            var bank = await _dataService.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }    

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(bank, new JsonSerializerOptions() { WriteIndented = true }));
            return File(new MemoryStream(bytes), "application/json", bank.Name + ".json");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dataService == null)
            {
                return NotFound();
            }

            var bank = await _dataService.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            await _dataService.DeleteAsync(bank);

            return RedirectToAction(nameof(Index));
        }

    }
}
