using MessagePack;

namespace MDSF.CrossDomains.Tasks.Customer.ConsumeService
{
    public static partial class ConsumeService
    {
        [MessagePackObject]
        public class ConsumeServiceRequest()
        {
            [Key(0)]
            public long ServiceId { get; set; }
            [Key(1)]
            public long UserId { get; set; }
        }
    }
}
