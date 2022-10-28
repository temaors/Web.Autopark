using System.Data;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AutoparkDAL.Repositories;

public class VehicleTypeRepository : IRepository<VehicleType>
{
    private string connectionString;

    public VehicleTypeRepository(string conn)
    {
        connectionString = conn;
    }
    
    public async Task<IEnumerable<VehicleType>> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return await db.QueryAsync<VehicleType>("SELECT * FROM VehicleTypes");
        }
    }

    public async Task<VehicleType> GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return await db.QueryFirstAsync<VehicleType>("SELECT * FROM VehicleTypes WHERE VehicletypeId = @id", new {id});
        }
    }

    public async Task Create(VehicleType item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO VehicleTypes (Name, TaxCoefficient)" +
                "VALUES (@Name, @TaxCoefficient)";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Update(VehicleType item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "UPDATE VehicleTypes SET Name = @Name, TaxCoefficient = @TaxCoefficient WHERE VehicleTypeId = @VehicleTypeId";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @id";
            await db.ExecuteAsync(sqlQuery, new { id });
        }
    }
}