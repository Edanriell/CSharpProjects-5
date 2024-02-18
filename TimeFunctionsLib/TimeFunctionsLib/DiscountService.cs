namespace Services;

public class DiscountService
{
    private TimeProvider _timeProvider;

    public DiscountService(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public decimal GetDiscount()
    {
        var now = _timeProvider.GetUtcNow();

        return now.DayOfWeek switch
        {
            DayOfWeek.Saturday or DayOfWeek.Sunday => 0.2M,
            _ => 0M
        };
    }
}
