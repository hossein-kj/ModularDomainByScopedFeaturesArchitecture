using MDSF.BuildingBlocks.Validation;

namespace MDSF.BuildingBlocks.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(ValidationResultModel validationResultModel)
        {
            ValidationResultModel = validationResultModel;
        }
        public ValidationResultModel ValidationResultModel { get; }
    }
}