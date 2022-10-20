using TrialBalanceWebApp.Entities;
using TrialBalanceWebApp.Repos;
using TrialBalanceWebApp.Repos.Base;
using TrialBalanceWebApp.Repos.Interfaces;
using TrialBalanceWebApp.Services.DataServices.Dal;
using TrialBalanceWebApp.Services.DataServices.Interfaces;

namespace TrialBalanceWebApp.Services.DataServices
{
    public static class DataServiceConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBankRepo, BankRepo>();
            return services;
        }
        public static IServiceCollection AddDataServices(
            this IServiceCollection services)
        {
                services.AddScoped<IDataServiceBase<Bank>, BankDalDataService>();
            return services;
        }
    }
}
