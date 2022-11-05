using System.ComponentModel.DataAnnotations;
using System.Drawing;
using AutoparkDAL.Interfaces;

namespace AutoparkDAL.Entities;

public class Vehicle
{
    [Key]
    public int VehicleId { get; set; }
    public int VehicleTypeId { get; set; }
    public VehicleType VehicleType { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public string RegistrationNumber { get; set; }
    [Required]
    [Range(1,Int32.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
    public double Weight { get; set; }
    [Required]
    [Range(1,2000, ErrorMessage = "Volume must be greater than 0.")]
    public int Volume { get; set; }
    [Required]
    [Range(1950,2022, ErrorMessage = "Year must be between {0} and {1}.")]
    public int Year { get; set; }
    [Required]
    [Range(1,Int32.MaxValue, ErrorMessage = "Mileage must be greater than 0")]
    public double Mileage { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    [Range(1,Int32.MaxValue, ErrorMessage = "Mileage must be greater than 0")]
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