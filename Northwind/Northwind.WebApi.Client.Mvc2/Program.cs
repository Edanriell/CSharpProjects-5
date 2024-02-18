using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Retry;
using System.Net.Http.Headers;
using static System.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

IEnumerable<TimeSpan> delays = Backoff.DecorrelatedJitterBackoffV2(
    medianFirstRetryDelay: TimeSpan.FromSeconds(1),
    retryCount: 5
);

WriteLine("Jittered delays for Polly retries:");
foreach (TimeSpan item in delays)
{
    WriteLine($"  {item.TotalSeconds:N2} seconds.");
}

AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(delays);

builder.Services
    .AddHttpClient(
        name: "Northwind.WebApi.Service",
        configureClient: options =>
        {
            options.BaseAddress = new("https://localhost:5091/");
            options.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json", 1.0)
            );
        }
    )
    .AddPolicyHandler(retryPolicy);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
