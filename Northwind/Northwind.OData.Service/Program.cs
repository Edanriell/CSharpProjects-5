using Microsoft.AspNetCore.OData;
using EntityModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNorthwindContext();

builder.Services
    .AddControllers()
    .AddOData(
        options =>
            options
                // Register two OData models.
                .AddRouteComponents(routePrefix: "catalog", model: GetEdmModelForCatalog())
                .AddRouteComponents(routePrefix: "ordersystem", model: GetEdmModelForOrderSystem())
                .AddRouteComponents(
                    routePrefix: "catalog/v{version}",
                    model: GetEdmModelForCatalog()
                )
                // Enable query options:
                .Select() // $select for projection.
                .Expand() // $expand to navigate to related entities.
                .Filter() // $filter.
                .OrderBy() // $orderby to sort.
                .SetMaxTop(100) // $top.
                .Count() // $count.
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
