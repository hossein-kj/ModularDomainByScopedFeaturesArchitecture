using MDSF.BuildingBlocks.Data.EFCore;
using MDSF.BuildingBlocks.Security;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MDSF.Customer.Data.EF;

internal class CustomerDbContext : AppDbContextBase, ICustomerDbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IAuthenticatedUser authenticatedUser)
        : base(options, authenticatedUser)
    {
    }

    public DbSet<Models.Customer> Customers => Set<Models.Customer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
