using GraphQL.Service;
using EntityModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNorthwindContext();

builder.Services
    .AddGraphQLServer()
    .AddFiltering()
    .AddSorting()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .RegisterDbContext<NorthwindContext>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.UseWebSockets();

app.MapGet("/", () => "Navigate to: https://localhost:5121/graphql");

app.MapGraphQL();

app.Run();
