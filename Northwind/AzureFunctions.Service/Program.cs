using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddHttpClient(
            name: "Amazon",
            configureClient: options =>
            {
                options.BaseAddress = new System.Uri("https://www.amazon.com");

                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("text/html")
                );
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/xhtml+xml")
                );
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/xml", 0.9)
                );
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("image/avif")
                );
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("image/webp")
                );
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("image/apng")
                );
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("*/*", 0.8)
                );

                options.DefaultRequestHeaders.AcceptLanguage.Add(
                    new StringWithQualityHeaderValue("en-US")
                );
                options.DefaultRequestHeaders.AcceptLanguage.Add(
                    new StringWithQualityHeaderValue("en", 0.8)
                );

                options.DefaultRequestHeaders.UserAgent.Add(
                    new(productName: "Chrome", productVersion: "114.0.5735.91")
                );
            }
        );
    })
    .Build();

host.Run();
