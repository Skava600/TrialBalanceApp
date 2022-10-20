using Microsoft.EntityFrameworkCore.Storage;

namespace TrialBalanceWebApp.Services.DataServices.Dal.Exceptions
{
    public class CustomRetryLimitExceedException : CustomException
    {
        public CustomRetryLimitExceedException() { }
        public CustomRetryLimitExceedException(string message)
        : base(message) { }
        public CustomRetryLimitExceedException(
        string message, RetryLimitExceededException innerException)
        : base(message, innerException) { }

    }
}
