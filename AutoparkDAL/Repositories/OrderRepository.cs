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
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<Order>("SELECT * FROM Orders").ToList();
        }
    }

    public Order GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<Order>("SELECT * FROM Orders WHERE OrderId = @id", new {id}).FirstOrDefault();
        }
    }

    public void Create(Order item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO Orders (VehicleId, Date)" +
                "VALUES (@VehicleId, @Date)";
            db.Execute(sqlQuery, item);
        }
    }

    public void Update(Order item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE Orders SET VehicleId = @VehicleId, Date = @Date";
            db.Execute(sqlQuery, item);
        }
    }

    public void Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM Orders WHERE OrderId = @id";
            db.Execute(sqlQuery, new { id });
        }
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}