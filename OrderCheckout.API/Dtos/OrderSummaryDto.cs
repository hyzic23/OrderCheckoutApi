namespace OrderCheckout.API.Dtos;

public class OrderSummaryDto
{
    public int OrderId { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string DiscountTier { get; set; } = string.Empty; // The Hard Constraint
    public List<OrderItemDto> Items { get; set; } = new();
}