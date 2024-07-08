using MDSF.BuildingBlocks.Data.EFCore;
using Microsoft.EntityFrameworkCore;

namespace MDSF.Customer.Data.EF
{
    internal interface ICustomerDbContext: IDbContext
    {
        DbSet<Models.Customer> Customers { get; }
    }
}