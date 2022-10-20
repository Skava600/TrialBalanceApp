using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TrialBalanceWebApp.Data;
using TrialBalanceWebApp.Models;
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
            var bank = await _dataService.FindAsync(id);

            return View(bank);
        }

        [HttpGet("download")]
        public async Task<IActionResult> Export(int? id, IFormFile file)
        {
            var bank = await _dataService.FindAsync(id);
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
