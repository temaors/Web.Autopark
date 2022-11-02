using System.ComponentModel.DataAnnotations;

namespace AutoparkDAL.Entities;

public class VehicleType
{
    public int VehicleTypeId { get; set; }
    public string Name { get; set; }
    [Range(0.1,100)]
    public double TaxCoefficient { get; set; }
}