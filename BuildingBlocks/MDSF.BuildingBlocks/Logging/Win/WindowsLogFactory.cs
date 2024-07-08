using Microsoft.Extensions.Logging;

namespace MDSF.BuildingBlocks.Logging.Win
{
    public class WindowsLogFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {

        }

        public ILogger CreateLogger(string categoryName)
        {
            return new WindowsLogger();
        }

        public void Dispose()
        {

        }
    }
}
