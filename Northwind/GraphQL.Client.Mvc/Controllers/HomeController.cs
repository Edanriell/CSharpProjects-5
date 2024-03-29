﻿using Microsoft.AspNetCore.Mvc;
using GraphQL.Client.Mvc.Models;
using System.Diagnostics;
using System.Text;

namespace GraphQL.Client.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index(string id = "1")
        {
            IndexViewModel model = new();

            try
            {
                HttpClient client = _clientFactory.CreateClient(name: "Northwind.GraphQL.Service");

                HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: "/");

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    model.Code = response.StatusCode;
                    model.Errors = new[]
                    {
                        new Error
                        {
                            Message = "Service is not successfully responding to GET requests."
                        }
                    };
                    return View(model);
                }

                request = new(method: HttpMethod.Post, requestUri: "graphql");

                request.Content = new StringContent(
                    content: $$$"""
{
  "query": "{productsInCategory(categoryId:{{{id}}}){productId productName unitsInStock}}"
}
""",
                    encoding: Encoding.UTF8,
                    mediaType: "application/json"
                );

                response = await client.SendAsync(request);

                model.Code = response.StatusCode;
                model.RawResponseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    model.Products = (await response.Content.ReadFromJsonAsync<ResponseProducts>())
                        ?.Data
                        ?.ProductsInCategory;
                }
                else
                {
                    model.Errors = (
                        await response.Content.ReadFromJsonAsync<ResponseErrors>()
                    )?.Errors;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Northwind.GraphQL service exception: {ex.Message}");

                model.Errors = new[] { new Error { Message = ex.Message } };
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
