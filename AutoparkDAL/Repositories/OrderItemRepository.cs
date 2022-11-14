using System.Data;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AutoparkDAL.Repositories;

public class OrderItemRepository : IRepository<OrderItem>
{
    private string connectionString; //make it readonly
    public OrderItemRepository(string conn)
    {
        connectionString = conn;
    }
    public async Task<IEnumerable<OrderItem>> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return await db.QueryAsync<OrderItem>("SELECT * FROM OrderItems");
        }
    }

    public async Task<OrderItem> GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return await db.QueryFirstAsync<OrderItem>("SELECT * FROM OrderItems WHERE OrderItemId = @id", new {id});
        }
    }

    public async Task Create(OrderItem item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO OrderItems (OrderId, ComponentId, Quantity)" + //you can use '@' instead of concatenation
                "VALUES (@OrderId, @ComponentId, @Quantity)";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Update(OrderItem item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE OrderItems SET OrderId = @OrderId, ComponentId = @ComponentId, Quantity = @Quantity";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM OrderItems WHERE OrderItemsId = @id";
            await db.ExecuteAsync(sqlQuery, new { id });
        }
    }
}