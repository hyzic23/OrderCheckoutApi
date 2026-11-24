using OrderCheckout.API.Models;

namespace OrderCheckout.API.Data;

public class DummyDataSeeder
{
    public static void SeedData(AppDbContext context)
    {
        context.Database.EnsureCreated();
        if (context.Orders.Any()) return;

        var order = new Order { Id = 1, CustomerName = "Acme Corp", OrderDate = DateTime.UtcNow };

        // Seed 50 items to trigger the N+1 issue
        for (int i = 1; i <= 50; i++)
        {
            order.OrderItems.Add(new OrderItem
            {
                Id = i,
                OrderId = 1,
                ProductName = $"Product-{i:D2}",
                Quantity = i % 3 + 1, 
                UnitPrice = 10.50m + i 
            });
        }

        context.Orders.Add(order);
        context.SaveChanges();
    }
}