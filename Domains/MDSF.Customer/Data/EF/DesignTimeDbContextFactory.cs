using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MDSF.Customer.Data.EF;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
{
    public CustomerDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<CustomerDbContext>();

        builder.UseSqlServer(
           "Server=(localdb)\\mssqllocaldb;Database=MDSF;" +
               "Trusted_Connection=True;MultipleActiveResultSets=true");
        return new CustomerDbContext(builder.Options, null);
    }
}
