namespace MDSF.BuildingBlocks.Logging.Web;

public class SentryOptions
{
    public bool Enabled { get; set; }
    public string Dsn { get; set; }
    public string MinimumBreadcrumbLevel { get; set; }
    public string MinimumEventLevel { get; set; }
}
