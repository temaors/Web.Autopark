using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;

namespace AutoparkDAL.Repositories;

public class VehicleTypeRepository : IRepository<VehicleType>
{
    private string connectionString;

    public VehicleTypeRepository(string conn)
    {
        connectionString = conn;
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<VehicleType> GetAll()
    {
        throw new NotImplementedException();
    }

    public VehicleType GetItem(int id)
    {
        throw new NotImplementedException();
    }

    public void Create(VehicleType item)
    {
        throw new NotImplementedException();
    }

    public void Update(VehicleType item)
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