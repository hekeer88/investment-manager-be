using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.WebApp;
using Tests.WebApp.Helpers;
using Xunit;

namespace Test.WebApp.Controller;

public class IntegrationTestPortfolioController : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public IntegrationTestPortfolioController(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }
        );
    }

    
    
    [Fact]
    public async Task Get_Portfolio_Index()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/api/v1/portfolios");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        
        var responseContent = await HtmlHelpers.GetDocumentAsync(response);
    }
}