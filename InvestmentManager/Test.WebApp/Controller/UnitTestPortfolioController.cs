using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.BLL;
using App.BLL.Services;
using App.Contracts.BLL;
using App.DAL.EF;
using App.DAL.EF.Mappers;
using App.DAL.EF.Repositories;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.ApiControllers;
using WebApp.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace Test.WebApp.Controller;

public class UnitTestPortfolioController
{
    private readonly ITestOutputHelper _testOutputHelper;
    // private readonly PortfoliosController _portfolioController;
    // private readonly AppBLL _appBll;
    private readonly AppUOW _appUow;

    private readonly PortfolioService _portfolioService;
    private readonly PortfolioRepository _portfolioRepository;
    private readonly AppDbContext _ctx;
    
    private readonly AutoMapper.IMapper _bllMapper;
    // private readonly Mock<IAppBLL> _bllMock;

    public UnitTestPortfolioController(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        // set up mock db - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger<PortfoliosController>();

        // _bllMock = new Mock<IAppBLL>();
        //
        // _bllMock.Setup(x => x.Meetings.GetAllAsync(true)).ReturnsAsync(new List<Meeting>()
        // {
        //     new Meeting()
        //     {
        //         Description = "TEST"
        //     }
        // });

        
        // _bllMapper = new Mapper()
        // var test = new PortfolioMapper(_bllMapper);

        // _portfolioRepository = new PortfolioRepository(_ctx, new PortfolioMapper());
        // _appUow = new AppUOW(_ctx);
        // _appBll = new AppBLL(_appUow, );
        // _portfolioController = new PortfoliosController(_appBll);
    }
    
    [Fact]
    public async Task IndexAction_ReturnsVmWithData()
    {
        // // Arrange
        // var testFooBar = new Portfolio() {Value = Guid.NewGuid().ToString()};
        // _ctx.FooBars.Add(testFooBar);
        // await _ctx.SaveChangesAsync();
        //
        // // Act
        // var result = (await _controller.Index()) as ViewResult;
        //
        // // Assert
        // Assert.NotNull(result);
        // Assert.NotNull(result!.Model);
        //
        // var model = result.Model as List<FooBar>;
        // Assert.NotNull(model);
        //
        // Assert.NotEmpty(model);
        // Assert.Single(model);
        // Assert.Equal(testFooBar.Value, model!.First().Value);
    }
    
}