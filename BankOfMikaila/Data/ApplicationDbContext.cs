using BankOfMikaila.Models;
using BankOfMikaila.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace BankOfMikaila.Data
{
    public class ApplicationDbContext : DbContext
    {
        public  ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<P2P> P2P { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    FirstName = "Andy",
                    LastName = "Zhang",
                   
                }
                );

            modelBuilder.Entity<Address>().HasData(
                new Address()
                {
                    Id = 1,
                    StreetNumber = "117",
                    StreetName = "Ballymeade",
                    City = "Wilmington",
                    State = "DE",
                    ZipCode = "19810",
                    CustomerId = 1
                }
                );

            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.Address)
                .WithOne(address => address.Customer)
                .HasForeignKey(address => address.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasOne(account => account.Customer)
                .WithMany()
                .HasForeignKey(account => account.CustomerId);

            modelBuilder.Entity<Transaction>()
                .HasOne(p => p.Account)
                .WithMany()
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.NoAction); // Adjust cascade behavior as needed

            modelBuilder.Entity<P2P>()
                .HasOne(p => p.Receiver)
                .WithMany()
                .HasForeignKey(p => p.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasDiscriminator(t => t.TransactionType)
                .HasValue<Deposit>(TransactionType.DEPOSIT)
                .HasValue<Withdrawal>(TransactionType.WITHDRAWAL)
                .HasValue<P2P>(TransactionType.P2P)
                .HasValue<Transaction>(TransactionType.DEFAULT);

            base.OnModelCreating(modelBuilder);
        }
    }
}
