using Grpc.Service.Services;
using EntityModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc().AddJsonTranscoding();

builder.Services.AddNorthwindContext();

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<ShipperService>();
app.MapGrpcService<ProductService>();
app.MapGrpcService<EmployeeService>();

app.MapGet(
    "/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909"
);

app.Run();
