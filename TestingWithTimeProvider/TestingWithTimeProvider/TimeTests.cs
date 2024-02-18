using Services;
using Moq;

namespace TestingWithTimeProvider;

public class TimeTests
{
    [Fact]
    public void TestDiscountDuringWorkdays()
    {
        TimeProvider timeProvider = Mock.Of<TimeProvider>();

        Mock.Get(timeProvider)
            .Setup(s => s.GetUtcNow())
            .Returns(
                new DateTimeOffset(
                    year: 2023,
                    month: 11,
                    day: 7,
                    hour: 9,
                    minute: 30,
                    second: 0,
                    offset: TimeSpan.Zero
                )
            );

        DiscountService service = new(timeProvider);

        decimal discount = service.GetDiscount();

        Assert.Equal(0M, discount);
    }

    [Fact]
    public void TestDiscountDuringWeekends()
    {
        TimeProvider timeProvider = Mock.Of<TimeProvider>();

        Mock.Get(timeProvider)
            .Setup(s => s.GetUtcNow())
            .Returns(
                new DateTimeOffset(
                    year: 2023,
                    month: 11,
                    day: 4,
                    hour: 9,
                    minute: 30,
                    second: 0,
                    offset: TimeSpan.Zero
                )
            );

        DiscountService service = new(timeProvider);

        decimal discount = service.GetDiscount();

        Assert.Equal(0.2M, discount);
    }
}
