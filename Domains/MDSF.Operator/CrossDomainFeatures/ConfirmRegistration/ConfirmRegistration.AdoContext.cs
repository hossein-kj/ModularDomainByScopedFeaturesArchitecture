using MDSF.BuildingBlocks.Data.ADO;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MDSF.Operator.CrossDomainFeatures.ConfirmRegistration
{
    public static partial class ConfirmRegistration
    {
        internal interface IConfirmRegistrationRegistrationAdoContext
        {
            Task<bool> ConfirmRegisteration(long id,CancellationToken cancellationToken);
        }

        private class ConfirmRegistrationRegistrationAdoContext(IConfiguration configuration) : IConfirmRegistrationRegistrationAdoContext
        {
            public async Task<bool> ConfirmRegisteration(long id,CancellationToken cancellationToken)
            {
                var result = await SqlHelper.ExecuteNonQueryAsync(configuration.GetConnectionString(nameof(ConfirmRegistration)), System.Data.CommandType.StoredProcedure,
                    "[dbo].[ConfirmRegisteration]", cancellationToken, new[]
                    {
                        new SqlParameter("@id", SqlDbType.BigInt) {Value = id}

                    });
                return result > 0;
            }
        }
    }
}
