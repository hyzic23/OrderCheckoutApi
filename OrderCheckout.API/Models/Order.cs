namespace OrderCheckout.API.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    
    // Virtual enables Lazy Loading (The N+1 Trigger)
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}