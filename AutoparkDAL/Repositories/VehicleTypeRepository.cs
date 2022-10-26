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
    
    public IEnumerable<VehicleType> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<VehicleType>("SELECT * FROM VehicleTypes").ToList();
        }
    }

    public VehicleType GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<VehicleType>("SELECT * FROM VehicleTypes WHERE VehicletypeId = @id", new {id}).FirstOrDefault();
        }
    }

    public void Create(VehicleType item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO VehicleTypes (Name, TaxCoefficient)" +
                "VALUES (@Name, @TaxCoefficient)";
            db.Execute(sqlQuery, item);
        }
    }

    public void Update(VehicleType item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE VehicleTypes SET Name = @Name, TaxCoefficient = @TaxCoefficient";
            db.Execute(sqlQuery, item);
        }
    }

    public void Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @id";
            db.Execute(sqlQuery, new { id });
        }
    }
}