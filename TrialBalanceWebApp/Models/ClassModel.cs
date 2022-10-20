namespace TrialBalanceWebApp.Models
{
    public class ClassModel
    {
        public string Name { get; set; }
        public IEnumerable<AccountModel> Accounts { get; set; }
    }
}
