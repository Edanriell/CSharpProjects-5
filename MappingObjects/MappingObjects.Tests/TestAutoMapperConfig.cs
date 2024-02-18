using AutoMapper;
using MappingObjects.Mappers;
using EntityModels;
using ViewModels;

namespace MappingObjects.Tests;

public class TestAutoMapperConfig
{
    [Fact]
    public void TestSummaryMapping()
    {
        MapperConfiguration config = CartToSummaryMapper.GetMapperConfiguration();

        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void TestSummaryMappingValues()
    {
        Cart cart =
            new(
                Customer: new(FirstName: "John", LastName: "Smith"),
                Items: new()
                {
                    new(ProductName: "Apples", UnitPrice: 0.49M, Quantity: 10),
                    new(ProductName: "Bananas", UnitPrice: 0.99M, Quantity: 4)
                }
            );

        MapperConfiguration config = CartToSummaryMapper.GetMapperConfiguration();

        IMapper mapper = config.CreateMapper();

        Summary summary = mapper.Map<Cart, Summary>(cart);

        Assert.True(summary.FullName == "John Smith");
        Assert.True(summary.Total == 8.86M);
    }
}
