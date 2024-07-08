using MessagePack;

namespace MDSF.CrossDomains.Tasks.Customer.ConsumeService
{
    public static partial class ConsumeService
    {
        [MessagePackObject]
        public class ConsumeServiceResponse()
        {
            [Key(0)]
            public bool Result { get; set; }
        }
    }
}
