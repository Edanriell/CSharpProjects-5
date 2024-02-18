using Microsoft.Data.SqlClient;
using Hangfire;
using Background.Models;
using Microsoft.AspNetCore.Mvc;

SqlConnectionStringBuilder connection = new();

connection.InitialCatalog = "Northwind.HangfireDb";
connection.MultipleActiveResultSets = true;
connection.Encrypt = true;
connection.TrustServerCertificate = true;
connection.ConnectTimeout = 5;
connection.DataSource = ".";

connection.IntegratedSecurity = true;

/*
// To use SQL Server authentication.
builder.UserID = "sa";
builder.Password = "123456";
builder.PersistSecurityInfo = false;
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(
    config =>
        config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connection.ConnectionString)
);

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseHangfireDashboard();

app.MapGet("/", () => "Navigate to /hangfire to see the Hangfire Dashboard.");

app.MapPost(
    "/schedulejob",
    ([FromBody] WriteMessageJobDetail job) =>
    {
        BackgroundJob.Schedule(
            methodCall: () => WriteMessage(job.Message),
            enqueueAt: DateTimeOffset.UtcNow + TimeSpan.FromSeconds(job.Seconds)
        );
    }
);

app.MapHangfireDashboard();

app.Run();
