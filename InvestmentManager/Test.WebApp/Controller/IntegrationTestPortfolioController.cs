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
using NuGet.Common;
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
    public async Task Get_Portfolios_UnAuthorized()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/api/v1/portfolios");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        await Post_Register();
    }
    
    
    public async Task Post_Register()
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
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Token);

        var apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Get;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.RequestUri = new Uri("/api/v1/portfolios/");
        
        // Act
        var apiResponse = await _client.SendAsync(apiRequest);
        
        // Assert
        apiResponse.EnsureSuccessStatusCode();
        
        var content = await apiResponse.Content.ReadAsStringAsync();
        var resultData = System.Text.Json.JsonSerializer.Deserialize<List<Portfolio>>(content);
        Assert.NotNull(resultData);
        Assert.Empty(resultData);
        
        await Post_New_Portfolio();
    }
    
    public async Task Post_New_Portfolio()
    {
        
        // Arrange
        var newPortfolio = new Portfolio()
        {
            Name = "New Test Portfolio",
            Description = "Test Description",
        };
        
        var portfolioStr = System.Text.Json.JsonSerializer.Serialize(newPortfolio);
        var portfolio = new StringContent(portfolioStr, Encoding.UTF8, "application/json");

        
        var apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Post;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.RequestUri = new Uri("/api/v1/portfolios/");
        apiRequest.Content = portfolio;
        
        //Act
        var apiResponse = await _client.SendAsync(apiRequest);
        
        // Assert
        apiResponse.EnsureSuccessStatusCode();
        
        var content = await apiResponse.Content.ReadAsStringAsync();
        var resultData = System.Text.Json.JsonSerializer.Deserialize<Portfolio>(content);
        var id = resultData?.Id;
        Assert.NotNull(resultData);
        
        await Get_Portfolios();
    }

    public async Task Get_Portfolios()
    {
        // Arrange
        var apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Get;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.RequestUri = new Uri("/api/v1/portfolios/");
        
        
        // Act
        var apiResponse = await _client.SendAsync(apiRequest);

        // Assert
        apiResponse.EnsureSuccessStatusCode();
        var content = await apiResponse.Content.ReadAsStringAsync();
        var resultData = System.Text.Json.JsonSerializer.Deserialize<List<Portfolio>>(content);
        Assert.NotNull(resultData);
        Assert.Single(resultData);
        
        await Post_Second_Portfolio();
    }

    public async Task? Post_Second_Portfolio()
    {
        
        // Arrange
        var newPortfolio = new Portfolio()
        {
            Name = "Second Test Portfolio",
            Description = "2nd Description",
        };
        
        var portfolioStr = System.Text.Json.JsonSerializer.Serialize(newPortfolio);
        var portfolio = new StringContent(portfolioStr, Encoding.UTF8, "application/json");

        
        var postRequest = new HttpRequestMessage();
        postRequest.Method = HttpMethod.Post;
        postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        postRequest.RequestUri = new Uri("/api/v1/portfolios/");
        postRequest.Content = portfolio;
        
        var getRequest = new HttpRequestMessage();
        getRequest.Method = HttpMethod.Get;
        getRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        getRequest.RequestUri = new Uri("/api/v1/portfolios/");
        
        //Act
        var postResponse = await _client.SendAsync(postRequest);
        var getResponse = await _client.SendAsync(getRequest);
        
        // Assert
        postResponse.EnsureSuccessStatusCode();
        getResponse.EnsureSuccessStatusCode();
        
        getResponse.EnsureSuccessStatusCode();
        var content = await getResponse.Content.ReadAsStringAsync();
        var resultData = System.Text.Json.JsonSerializer.Deserialize<List<Portfolio>>(content);
        
        Assert.NotNull(resultData);
        Assert.Equal(2, resultData.Count);
    }
}