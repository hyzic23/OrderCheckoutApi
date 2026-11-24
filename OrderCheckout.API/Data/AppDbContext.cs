using Microsoft.EntityFrameworkCore;
using OrderCheckout.API.Models;

namespace OrderCheckout.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}