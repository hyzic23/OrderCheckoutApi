using Microsoft.AspNetCore.Mvc;
using OrderCheckout.API.Services;

namespace OrderCheckout.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}/summary")]
    public async Task<IActionResult> GetOrderSummary(int id)
    {
        var summary = await _orderService.GetOrderSummaryAsync(id);
        if (summary == null) return NotFound();
        return Ok(summary);
    }
}