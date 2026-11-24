using Microsoft.EntityFrameworkCore;
using OrderCheckout.API.Data;
using OrderCheckout.API.Dtos;

namespace OrderCheckout.API.Services;

public class EfSplitQueryRepository  : IOrderService
{
    private readonly AppDbContext _context;

    public EfSplitQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderSummaryDto> GetOrderSummaryAsync(int orderId)
    {
        //Fetch entities using Sp;it Query (2 SQL queries total)
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);
        
        if (order == null) return null;
        
        //Map in memory to allow the complex calculation
        var totalAmount = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
        
        return new OrderSummaryDto
        {
            OrderId = order.Id,
            Customer = order.CustomerName,
            TotalAmount = totalAmount,
            DiscountTier = DiscountCalculator.GetDiscountTier(totalAmount),
            Items = order.OrderItems.Select(i => new OrderItemDto
            {
                Product = i.ProductName,
                Quantity = i.Quantity,
                Price = i.UnitPrice
            }).ToList()
        };
    }
    
}