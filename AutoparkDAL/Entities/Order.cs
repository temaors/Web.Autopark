namespace AutoparkDAL.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int VehicleId { get; set; }
    public DateTime Date { get; set; }
}