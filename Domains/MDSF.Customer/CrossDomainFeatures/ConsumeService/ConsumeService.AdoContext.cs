using MDSF.BuildingBlocks.Data.ADO;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MDSF.Customer.CrossDomainFeatures.ConsumeService
{
    public static partial class ConsumeService
    {
        internal interface IConsumeServiceAdoContext
        {
            Task<bool> ConsumeService(long serviceId, long customerId, CancellationToken cancellationToken);
        }

        private class ConsumeServiceAdoContext(IConfiguration configuration) : IConsumeServiceAdoContext
        {
            public async Task<bool> ConsumeService(long serviceId, long customerId, CancellationToken cancellationToken)
            {
                var result = await SqlHelper.ExecuteNonQueryAsync(configuration.GetConnectionString(nameof(ConsumeService)),
                    CommandType.StoredProcedure,
                    "[dbo].[ConsumeService]", cancellationToken, new[]
                    {
                        new SqlParameter("@serviceId", SqlDbType.BigInt) {Value = serviceId},
                        new SqlParameter("@customerId", SqlDbType.BigInt) {Value = customerId}

                    });
                return result > 0;
            }
        }
    }
}
