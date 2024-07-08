using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MDSF.Customer.Data.EF.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Models.Customer>
{
    public void Configure(EntityTypeBuilder<Models.Customer> builder)
    {
        builder.ToTable("Customer", "dbo");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.OwnsOne(c => c.Account, x =>
        {
            x.Property(c => c.AccountNo);
            x.Property(c => c.AccountId);
        });

        builder.OwnsOne(c => c.CustomerInfo, x =>
        {
            x.Property(c => c.Name);
        });
    }
}
