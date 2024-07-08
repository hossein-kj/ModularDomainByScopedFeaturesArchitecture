namespace MDSF.BuildingBlocks.Caching
{
    public interface IInvalidateCacheRequest
    {
        string CacheKey { get; }
    }
}
