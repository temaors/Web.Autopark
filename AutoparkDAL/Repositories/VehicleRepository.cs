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
    public async Task<IEnumerable<Vehicle>> GetAll()
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return await db.QueryAsync<Vehicle>("SELECT * FROM Vehicles");
        }
    }

    public async Task<Vehicle> GetItem(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            return await db.QueryFirstAsync<Vehicle>("SELECT * FROM Vehicles WHERE VehicleId = @id", new {id});
        }
    }

    public async Task Create(Vehicle item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "INSERT INTO Vehicles (VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption)" +
                "VALUES (@VehicleTypeId, @Model, @RegistrationNumber, @Weight, @Year, @Mileage, @Color, @FuelConsumption)";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Update(Vehicle item)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery =
                "UPDATE Vehicles SET VehicleType = @VehicleType, Model = @Model, RegistrationNumber = @RegistrationNumber, Weight = @Weight, Year = @Year, Mileage = @Mileage, Color = @Color, FuelConsumption = @FuelConsumption";
            await db.ExecuteAsync(sqlQuery, item);
        }
    }

    public async Task Delete(int id)
    {
        using (IDbConnection db = new SqlConnection(connectionString))
        {
            var sqlQuery = "DELETE FROM Vehicles WHERE VehicleId = @id";
            await db.ExecuteAsync(sqlQuery, new { id });
        }
    }
}