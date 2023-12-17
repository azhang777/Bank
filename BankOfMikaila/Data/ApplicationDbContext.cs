using Microsoft.EntityFrameworkCore;

namespace BankOfMikaila.Data
{
    public class ApplicationDbContext : DbContext
    {
        public  ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
    }
}
