using MediatR;

namespace MDSF.BuildingBlocks.MediatR
{
    public class MediatRService : IMediatRService
    {
        private readonly ISender _sender;
        public MediatRService(ISender sender)
        {
            _sender = sender;
        }

        public async Task<T> ExecuteAsync<T>(IRequest<T> task, CancellationToken cancellationToken)
        {
            return await _sender.Send(task, cancellationToken);
        }

        //public async Task<Appliance> Add(Appliance appliance)
        //{
        //    _productContext.Add<Appliance>(appliance);
        //    await _productContext.SaveChangesAsync();
        //    return appliance;
        //}

        //public async Task<Appliance> Update(Appliance appliance)
        //{
        //    _productContext.Update<Appliance>(appliance);
        //    await _productContext.SaveChangesAsync();
        //    return appliance;
        //}
    }
}
