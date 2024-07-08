using Ardalis.GuardClauses;
using MDSF.BuildingBlocks.CAP;
using MDSF.BuildingBlocks.Dtos;
using MDSF.BuildingBlocks.Globalization;
using MDSF.Customer.Data.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MDSF.Customer.Features.Registration
{
    public static partial class Registration
    {
        private class CreateCustomerCommandHandler(ICustomerDbContext customerDbContext,
            ITextResource textResource, IBusPublisher busPublisher)
            : IRequestHandler<CreateCustomerCommand, TResult<CreateCustomerResponse>>
        {
            public async Task<TResult<CreateCustomerResponse>>
                Handle(CreateCustomerCommand request,
                CancellationToken cancellationToken)
            {
                Guard.Against.Null(request, nameof(request));

                var customer = Models.Customer.Create(request.Id, request.Request.CustomerInfo, request.Request.Account);
                await customerDbContext.Customers.AddAsync(customer);
                await customerDbContext.SaveChangesAsync();

                var domainEvents = customer.DomainEvents;
                await busPublisher.SendAsync(domainEvents.ToArray(), cancellationToken);

                return new TResult<CreateCustomerResponse>
                {
                    Value = new CreateCustomerResponse(customer.Id)
                };

            }
        }

        private class GetAvailableRegistrationQueryHandler(ICustomerDbContext customerDbContext)
           : IRequestHandler<GetAvailableRegistrationQuery, TResult<GetAvailableRegistrationQueryResponse>>
        {
            public async Task<TResult<GetAvailableRegistrationQueryResponse>>
                Handle(GetAvailableRegistrationQuery request,
                CancellationToken cancellationToken)
            {
                Guard.Against.Null(request, nameof(request));


                var user = await customerDbContext.Customers.FirstOrDefaultAsync();




                return new TResult<GetAvailableRegistrationQueryResponse>
                {
                    Value = new GetAvailableRegistrationQueryResponse(user.Id)
                };

            }
        }
    }
}
