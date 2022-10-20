using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrialBalanceWebApp.Models;
using Microsoft.AspNetCore.Http;
using IronXL;
using TrialBalanceWebApp.Data;
using TrialBalanceWebApp.Helpers;

namespace TrialBalanceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Import(IFormFile excelFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var stream = excelFile.OpenReadStream())
                    {
                        var workbook = WorkBook.FromStream(stream);
                        foreach (var item in workbook.WorkSheets)
                        {
                            await _context.Banks.AddAsync(BankExcelReader.ReadExcelDoc(item));
                            
                        }

                        workbook.Close();

                        await _context.SaveChangesAsync();
                    }
                }
                 catch(Exception ex)
                {
                    _logger.LogInformation(ex, ex.Message);
                }
            }

            return RedirectToAction("Index");
        }
    }
}