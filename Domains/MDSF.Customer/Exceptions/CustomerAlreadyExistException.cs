using MDSF.BuildingBlocks.Exceptions;
using MDSF.BuildingBlocks.Globalization;
using System.Net;

namespace MDSF.Customer.Exceptions
{
    public class CustomerAlreadyExistException : BaseException
    {
        public CustomerAlreadyExistException(Guid id)
            : base(new TextResource().AlreadyExist(new TextResource().Customer, id), HttpStatusCode.AlreadyReported)
        {
        }
    }
}
