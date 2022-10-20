namespace TrialBalanceWebApp.Models.ViewModels
{
    public class BankViewModel
    {
        public string Name { get; set; }
        public IEnumerable<ClassModel> Classes { get; set; }
        public AccountModel TotalAccount { get; set; }
    }
}
