namespace AutoparkDAL.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ComponentId { get; set; }
    public int Quantity { get; set; }
    public Order Order { get; set; }
    public Component Component { get; set; }
}