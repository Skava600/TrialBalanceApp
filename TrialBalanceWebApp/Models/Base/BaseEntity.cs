using System.ComponentModel.DataAnnotations;

namespace TrialBalanceWebApp.Models.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
