using System.Data;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AutoparkDAL.Repositories;

public class OrderItemRepository : IRepository<OrderItem>
{
    private string connectionString;
    public OrderItemRepository(string conn)
    {
        connectionString = conn;
    }
    public IEnumerable<OrderItem> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<OrderItem>("SELECT * FROM OrderItems").ToList();
        }
    }

    public OrderItem GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<OrderItem>("SELECT * FROM OrderItems WHERE OrderItemId = @id", new {id}).FirstOrDefault();
        }
    }

    public void Create(OrderItem item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO OrderItem (OrderItemId, OrderId, ComponentId, Quantity)" +
                "VALUES (@OrderItemId, @OrderId, @ComponentId, @Quantity)";
            db.Execute(sqlQuery, item);
        }
    }

    public void Update(OrderItem item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE OrderItems SET OrderId = @OrderId, ComponentId = @ComponentId, Quantity = @Quantity";
            db.Execute(sqlQuery, item);
        }
    }

    public void Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM OrderItems WHERE OrderItemsId = @id";
            db.Execute(sqlQuery, new { id });
        }
    }
}