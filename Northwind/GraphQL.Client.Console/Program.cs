﻿using Microsoft.Extensions.DependencyInjection;
using Northwind.GraphQL.Client.Console;
using StrawberryShake;
using static System.Console;

ServiceCollection serviceCollection = new();

serviceCollection
    .AddNorthwindClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:5121/graphql"));

IServiceProvider services = serviceCollection.BuildServiceProvider();

INorthwindClient client = services.GetRequiredService<INorthwindClient>();

var result = await client.SeafoodProducts.ExecuteAsync();
result.EnsureNoErrors();

if (result.Data is null)
{
    WriteLine("No data!");
    return;
}

foreach (var product in result.Data.ProductsInCategory)
{
    WriteLine("{0}: {1}", product.ProductId, product.ProductName);
}
