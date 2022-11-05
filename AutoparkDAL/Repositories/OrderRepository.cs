using System.Data;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AutoparkDAL.Repositories;

public class OrderRepository : IRepository<Order>
{
    private string connectionString;

    public OrderRepository(string conn)
    {
        connectionString = conn;
    }
    public async Task<IEnumerable<Order>> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return await db.QueryAsync<Order>("SELECT * FROM Orders");
        }
    }

    public async Task<Order> GetItem(int id)
    {
        using IDbConnection db = new SqlConnection(connectionString);
        return await db.QueryFirstAsync<Order>("SELECT * FROM Orders WHERE OrderId = @id", new {id});
    }

    public async Task Create(Order item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO Orders (VehicleId, Date)" +
                "VALUES (@VehicleId, @Date)";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Update(Order item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE Orders SET VehicleId = @VehicleId, Date = @Date";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM Orders WHERE OrderId = @id";
            await db.ExecuteAsync(sqlQuery, new { id });
        }
    }
}