using OrderCheckout.API.Dtos;

namespace OrderCheckout.API.Services;

public interface IOrderService
{
    Task<OrderSummaryDto?> GetOrderSummaryAsync(int orderId);
}