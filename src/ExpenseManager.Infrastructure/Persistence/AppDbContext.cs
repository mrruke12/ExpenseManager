using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence {
    public class AppDbContext : DbContext {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<BankAccount> Accounts { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IBankTransferCategory> TransfersCategory { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            //Database.EnsureCreated();   // создаем бд с новой схемой
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Accounts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Categories)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccount>()
                .HasMany(a => a.Expenses)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccount>()
                .HasMany(a => a.Incomes)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
