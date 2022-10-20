using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TrialBalanceWebApp.Entities;
using TrialBalanceWebApp.Services.DataServices.Dal.Exceptions;
using TrialBalanceWebApp.Services.Logging.Interfaces;

namespace TrialBalanceWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IAppLogging<ApplicationDbContext> AppLogging;
        public DbSet<Bank> Banks { get; set; }
        public DbSet<AccountClass> AccountClasses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Revenue> Revenues { get; set; }

        public ApplicationDbContext(IAppLogging<ApplicationDbContext> appLogging, DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
            this.AppLogging = appLogging;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.AppLogging.LogAppError(ex, "A concurrency error happened.");
                throw new CustomConcurrencyException("A concurrency error happened.", ex);
            }
            catch (RetryLimitExceededException ex)
            {
                this.AppLogging.LogAppError(ex, "There is a problem with SQL Server.");
                throw new CustomRetryLimitExceedException("There is a problem with SQL Server.", ex);
            }
            catch (DbUpdateException ex)
            {
                this.AppLogging.LogAppError(ex, "An error occurred updating the database.");
                throw new CustomDbUpdateException("An error occurred updating the database", ex);
            }
            catch (Exception ex)
            {
                this.AppLogging.LogAppError(ex, "An error occurred updating the databas.");
                throw new CustomException("An error occurred updating the database", ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountClass>()
                .HasMany<Account>(t => t.Accounts)
                .WithOne()
                .HasForeignKey(t => t.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bank>()
                .HasMany<AccountClass>(b => b.AccountClasses)
                .WithOne()
                .HasForeignKey(t => t.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasOne<Balance>(a => a.OpeningBalance)
                .WithOne()
                .HasForeignKey<Account>(t => t.OpeningBalanceId);

            modelBuilder.Entity<Revenue>()
                .HasOne<Account>()
                .WithOne(t => t.Revenue)
                .HasForeignKey<Account>(t => t.RevenueId);
        }


    }
}
