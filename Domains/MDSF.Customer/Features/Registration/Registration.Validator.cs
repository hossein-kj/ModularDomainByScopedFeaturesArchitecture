using FluentValidation;
using MDSF.BuildingBlocks.Globalization;

namespace MDSF.Customer.Features.Registration;

public static partial class Registration
{
    internal class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerRequestValidator(ITextResource textResource)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotNull().WithMessage(textResource.ValueCanNotBeEmpty(textResource.Id));
            //RuleFor(x => x.Name).NotNull().WithMessage(textResource.ValueCanNotBeEmpty(textResource.Name));
        }
    }
}

