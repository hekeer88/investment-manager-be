using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.WebApp;
using Tests.WebApp.Helpers;
using WebApp.DTO.Identity;
using Xunit;
using Portfolio = App.Public.DTO.v1.Portfolio;

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
        
        // var responseContent = await HtmlHelpers.GetDocumentAsync(response);
        
        //  add .id-table-foobars-row in table class tag
        // var tableRows = responseContent.QuerySelectorAll(".id-table-foobars-row");
    }
    
    [Fact]
    public async Task Get_Portfolio_Test()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/api/v1/portfolios");

        var content = await response.Content.ReadAsStringAsync();

        
        // maybe domain.Portfolio
        var resultData = System.Text.Json.JsonSerializer.Deserialize<List<Portfolio>>(content);
        
        Assert.NotNull(resultData);
        Assert.Single(resultData);
    }
    
    
    
    [Fact]
    public async Task Get_FooBars_API_Returns_Single_Element()
    {
        // Arrange
        var registerDto = new Register()
        {
            Email = "test@test.test",
            Password = "Test1.test",
            FirstName = "Test",
            LastName = "TestLast"
        };
        var jsonStr = System.Text.Json.JsonSerializer.Serialize(registerDto);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/v1/identity/Account/Register/", data);

        response.EnsureSuccessStatusCode();

        var requestContent = await response.Content.ReadAsStringAsync();
        
        var resultJwt = System.Text.Json.JsonSerializer.Deserialize<JwtResponse>(
        requestContent,
        new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );
        //
        //
        var apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Get;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Token);
        apiRequest.RequestUri = new Uri("/api/v1/portfolios/");
        
        
        //Act
        var apiResponse = await _client.SendAsync(apiRequest);
        
        // Assert
        apiResponse.EnsureSuccessStatusCode();
        
        var content = await apiResponse.Content.ReadAsStringAsync();
        var resultData = System.Text.Json.JsonSerializer.Deserialize<List<Portfolio>>(content);
        Assert.NotNull(resultData);
        Assert.Empty(resultData);
    }
    
    
    
}