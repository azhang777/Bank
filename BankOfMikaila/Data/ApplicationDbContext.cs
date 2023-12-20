using BankOfMikaila.Models;
using Microsoft.EntityFrameworkCore;

namespace BankOfMikaila.Data
{
    public class ApplicationDbContext : DbContext
    {
        public  ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
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
                .HasOne(p => p.Account1)
                .WithMany()
                .HasForeignKey(p => p.Account1_Id)
                .OnDelete(DeleteBehavior.NoAction); // Adjust cascade behavior as needed

            modelBuilder.Entity<Transaction>()
                .HasOne(p => p.Account2)
                .WithMany()
                .HasForeignKey(p => p.Account2_Id)
                .OnDelete(DeleteBehavior.NoAction); // Adjust cascade behavior as needed
        }
    }
}
