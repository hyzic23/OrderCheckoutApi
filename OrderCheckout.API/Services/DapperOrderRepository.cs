using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrderCheckout.API.Data;
using OrderCheckout.API.Dtos;
using OrderCheckout.API.Models;

namespace OrderCheckout.API.Services;

public class DapperOrderRepository : IOrderService
{
    private readonly string _connectionString;
    private readonly AppDbContext _context;

    public DapperOrderRepository(AppDbContext context)
    {
        _context = context;
        _connectionString = context.Database.GetDbConnection().ConnectionString;
    }

    public async Task<OrderSummaryDto?> GetOrderSummaryAsync(int orderId)
    {
        using var connection = new SqliteConnection(_connectionString);
        
        //1. Fetch Order
        var orderSql = "SELECT Id, CustomerName FROM Orders WHERE Id = @OrderId";
        var order = await connection.QuerySingleOrDefaultAsync<Order>(orderSql, new { orderId = orderId });

        if (order == null) return null;
        
        //2. Fetch Order Items
        var itemsSql = "SELECT ProductName, Quantity, UnitPrice FROM OrderItems WHERE OrderId = @OrderId";
        var items = (await connection.QueryAsync<OrderItem>(itemsSql, new { OrderId = orderId })).ToList();
        
        //3. Calculate and Map
        var totalAmount = items.Sum(i => i.Quantity * i.UnitPrice);

        return new OrderSummaryDto
        {
            OrderId = order.Id,
            Customer = order.CustomerName,
            TotalAmount = totalAmount,
            DiscountTier = DiscountCalculator.GetDiscountTier(totalAmount),
            Items = items.Select(i => new OrderItemDto
            {
                Product = i.ProductName, 
                Quantity = i.Quantity, 
                Price = i.UnitPrice
            }).ToList()
        };

    }
}