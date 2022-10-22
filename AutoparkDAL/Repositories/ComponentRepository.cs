using System.Data;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AutoparkDAL.Repositories;

public class ComponentRepository : IRepository<Component>
{
    private string connectionString;
    public ComponentRepository(string conn)
    {
        connectionString = conn;
    }
    public IEnumerable<Component> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<Component>("SELECT * FROM Components").ToList();
        }
    }

    public Component GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<Component>("SELECT * FROM Components WHERE ComponentId = @id", new {id}).FirstOrDefault();
        }
    }

    public void Create(Component item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO Componets (ComponentId, Name)" +
                "VALUES (@ComponentId, @Name)";
            db.Execute(sqlQuery, item);
        }
    }

    public void Update(Component item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE Components SET Name = @Name";
            db.Execute(sqlQuery, item);
        }
    }

    public void Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM Components WHERE ComponentId = @id";
            db.Execute(sqlQuery, new { id });
        }
    }
}