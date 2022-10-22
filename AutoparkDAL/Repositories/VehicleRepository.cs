using System.Data;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AutoparkDAL.Repositories;

public class VehicleRepository : IRepository<Vehicle>
{
    private string connectionString;
    public VehicleRepository(string conn)
    {
        connectionString = conn;
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Vehicle> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<Vehicle>("SELECT * FROM Vehicles").ToList();
        }
    }

    public Vehicle GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return db.Query<Vehicle>("SELECT * FROM Vehicles WHERE VehicleId = @id", new {id}).FirstOrDefault();
        }
    }

    public void Create(Vehicle item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO Vehicles (VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption)" +
                "VALUES (@VehicleTypeId, @Model, @RegistrationNumber, @Weight, @Year, @Mileage, @Color, @FuelConsumption)";
            db.Execute(sqlQuery, item);
        }
    }

    public void Update(Vehicle item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE Vehicles SET VehicleType = @VehicleType, Model = @Model, RegistrationNumber = @RegistrationNumber, Weight = @Weight, Year = @Year, Mileage = @Mileage, Color = @Color, FuelConsumption = @FuelConsumption";
            db.Execute(sqlQuery, item);
        }
    }

    public void Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM Vehicles WHERE VehicleId = @id";
            db.Execute(sqlQuery, new { id });
        }
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}