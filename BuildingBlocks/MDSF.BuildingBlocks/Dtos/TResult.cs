using MDSF.BuildingBlocks.Globalization;

namespace MDSF.BuildingBlocks.Dtos
{
    public record TResult<T>
    {
        public T? Value { get; set; }
        public string Message { get; set; } = new TextResource().TheOprationSuccessfullyComplete;
        public int StatusCode { get; set; } = 200;

    }
}
