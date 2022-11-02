using System.Drawing;
using AutoparkDAL.Interfaces;

namespace AutoparkDAL.Entities;

public class Vehicle
{
    public int VehicleId { get; set; }
    public int VehicleTypeId { get; set; }
    public VehicleType VehicleType { get; set; }
    public string Model { get; set; }
    public string RegistrationNumber { get; set; }
    public double Weight { get; set; }
    public int Volume { get; set; }
    public int Year { get; set; }
    public double Mileage { get; set; }
    public string Color { get; set; }
    public double FuelConsumption { get; set; }
    
    public double GetCalcTaxPerMonth()
    {
        return Weight * 0.0013 + VehicleType.TaxCoefficient * 30 + 5;
    }

    public double GetMaxKilometers()
    {
        return Volume / FuelConsumption * 100;
    }
}