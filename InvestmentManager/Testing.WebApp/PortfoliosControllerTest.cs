using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using WebApp.ApiControllers;
using Xunit;

namespace Testing.WebApp;

public class PortfoliosControllerTest
{
    private readonly PortfoliosController _portfoliosController;
        
    public PortfoliosControllerTest()
    {
        using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = logFactory.CreateLogger<PortfoliosController>();

        // _portfoliosController = new PortfoliosController(logger);
    }
    
    [Fact]
    public void Test1()
    {
        var result = _portfoliosController.GetPortfolios();
        Assert.NotNull(result);
        result.Should().NotBeNull();
    }
}