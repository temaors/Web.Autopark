using System.Data;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AutoparkDAL.Repositories;

public class ComponentRepository : IRepository<Component>
{
    private string connectionString; //make it readonly
    public ComponentRepository(string conn)
    {
        connectionString = conn;
    }
    public async Task<IEnumerable<Component>> GetAll()
    {
        using IDbConnection db = new SqlConnection(connectionString);
        return await db.QueryAsync<Component>("SELECT * FROM Components");

    }

    public async Task<Component> GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return await db.QueryFirstAsync<Component>("SELECT * FROM Components WHERE ComponentId = @id", new {id});
        }
    }

    public async Task Create(Component item)
    {
        using IDbConnection db = new SqlConnection(connectionString);
        var sqlQuery = "INSERT INTO Components (Name) VALUES (@Name)";
        await db.ExecuteAsync(sqlQuery, item);
    }

    public async Task Update(Component item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE Components SET Name = @Name";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM Components WHERE ComponentId = @id";
            await db.ExecuteAsync(sqlQuery, new { id });
        }
    }
}