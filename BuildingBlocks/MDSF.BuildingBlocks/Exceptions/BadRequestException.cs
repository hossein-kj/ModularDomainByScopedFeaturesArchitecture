using System;

namespace MDSF.BuildingBlocks.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}