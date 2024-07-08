namespace MDSF.BuildingBlocks.Data.EFCore
{
    public interface IDataSeeder
    {
        Task SeedAllAsync<TContext>();
    }
}