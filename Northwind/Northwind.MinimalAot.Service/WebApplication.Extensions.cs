using Microsoft.Data.SqlClient;
using Models;
using System.Data;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

public static class WebApplicationExtensions
{
    private static string _policyName = "fixed5per10seconds";

    public static void UseCustomRateLimiting(this WebApplication app)
    {
        RateLimiterOptions rateLimiterOptions = new();

        rateLimiterOptions.AddFixedWindowLimiter(
            policyName: _policyName,
            options =>
            {
                options.PermitLimit = 5;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 2;
                options.Window = TimeSpan.FromSeconds(10);
            }
        );

        app.UseRateLimiter(rateLimiterOptions);
    }

    public static void MapGets(this WebApplication app)
    {
        // app.MapGet(pattern, handler);

        app.MapGet("/", () => "Hello from a native AOT minimal API web service.");

        app.MapGet("/products", GetProducts).RequireRateLimiting(policyName: _policyName);

        app.MapGet("/products/{minimumUnitPrice:decimal?}", GetProducts);
    }

    private static List<Product> GetProducts(decimal? minimumUnitPrice = null)
    {
        SqlConnectionStringBuilder builder = new();

        builder.InitialCatalog = "Northwind";
        builder.MultipleActiveResultSets = true;
        builder.Encrypt = true;
        builder.TrustServerCertificate = true;
        builder.ConnectTimeout = 10;
        builder.DataSource = ".";
        builder.IntegratedSecurity = true;

        /*
        // To use SQL Server Authentication:
        builder.UserID = userId;
        builder.Password = password;
        builder.PersistSecurityInfo = false;
        */

        SqlConnection connection = new(builder.ConnectionString);

        connection.Open();

        SqlCommand cmd = connection.CreateCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT ProductId, ProductName, UnitPrice FROM Products";

        if (minimumUnitPrice.HasValue)
        {
            cmd.CommandText += " WHERE UnitPrice >= @minimumUnitPrice";

            cmd.Parameters.AddWithValue("minimumUnitPrice", minimumUnitPrice);
        }

        SqlDataReader r = cmd.ExecuteReader();

        List<Product> products = new();

        while (r.Read())
        {
            Product p =
                new()
                {
                    ProductId = r.GetInt32("ProductId"),
                    ProductName = r.GetString("ProductName"),
                    UnitPrice = r.GetDecimal("UnitPrice")
                };
            products.Add(p);
        }

        r.Close();

        return products;
    }
}
