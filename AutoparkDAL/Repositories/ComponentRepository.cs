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
    public void Dispose()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void Update(Component item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}