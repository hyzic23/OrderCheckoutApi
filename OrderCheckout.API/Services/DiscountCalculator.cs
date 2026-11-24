namespace OrderCheckout.API.Services;

public static class DiscountCalculator
{
    // EF Core will throw an exception if you try to call this inside a .Select() projection
    public static string GetDiscountTier(decimal totalAmount)
    {
        if (totalAmount > 2000m) return "Platinum";
        if (totalAmount > 1500m) return "Gold";
        if (totalAmount > 1000m) return "Silver";
        return "Standard";
    }
}