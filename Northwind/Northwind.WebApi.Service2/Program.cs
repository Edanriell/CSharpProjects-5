using Microsoft.Extensions.Caching.Memory;
using EntityModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMemoryCache>(
    new MemoryCache(new MemoryCacheOptions { TrackStatistics = true, SizeLimit = 50 })
);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddResponseCaching();

builder.Services.AddNorthwindContext();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
