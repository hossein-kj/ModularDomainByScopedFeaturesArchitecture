using System.Net;

namespace MDSF.BuildingBlocks.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public BaseException(
            string message,
            System.Exception innerException,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public BaseException(
            HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base()
        {
            StatusCode = statusCode;
        }

    }
}
