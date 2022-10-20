using IronXL;
using System.Linq;
using TrialBalanceWebApp.Models;
using TrialBalanceWebApp.Helpers;
using System.Web;

namespace TrialBalanceWebApp.Data
{
    public class DbInitiailizer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DbInitiailizer(ApplicationDbContext applicationDbContext, ILogger<DbInitiailizer> logger)
        {
            _context = applicationDbContext;
            _logger = logger;
        }
        public async Task Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Banks.Any())
            {
                try
                {
                    string bankInitDataPath = Directory.GetCurrentDirectory() + "\\SpreadSheets\\Bank1Data.xls";

                    Bank bank;
                    var workbook = WorkBook.LoadExcel(bankInitDataPath);

                    bank = BankExcelReader.ReadExcelDoc(workbook.GetWorkSheet("Sheet1"));

                    workbook.Close();
                    _context.Banks.Add(bank);

                    await _context.SaveChangesAsync();
                }
                catch( Exception ex)
                {
                    _logger.LogInformation(ex, ex.Message);
                }
                
            }
        }
    }
}
