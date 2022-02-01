using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Matrix.AppSample.Domain.Travels.DTOs;
using Matrix.AppSample.SeedWork;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Matrix.AppSample.Domain.Travels.Infrastructure
{
    public sealed class TravelsQueries : IQuery<TravelsQueries>
    {
        private readonly string _connectionString;

        public TravelsQueries(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<TravelListViewModel>> GetTravelsListAsync(int page, int take, string filter, CancellationToken token)
        {
            //TODO : Prepapar sql mais performatico para entregar a view model pronta
            var sql = "SELEC * FROM TESTE";
            await using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<TravelListViewModel>(sql, new { });
        }

        public async Task<TravelDetailViewModel> GetTravelDetailByIdAsync(int id, CancellationToken cancellationToken)
        {
            //TODO : Prepapar sql mais performatico para entregar a view model pronta
            var sql = "SELEC * FROM TESTE WHERE Id = @id";
            await using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync(sql, new { id });
        }
    }
}