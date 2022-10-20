using Microsoft.EntityFrameworkCore;

namespace TrialBalanceWebApp.Services.DataServices.Dal.Exceptions
{
    public class CustomDbUpdateException : CustomException
    {
        public CustomDbUpdateException() { }
        public CustomDbUpdateException(string message) : base(message) { }
        public CustomDbUpdateException(
        string message, DbUpdateException innerException)
        : base(message, innerException) { }
    }
}

