using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL;
using App.BLL.Services;
using App.Contracts.BLL;
using App.DAL.EF;
using App.DAL.EF.Mappers;
using App.DAL.EF.Repositories;
using App.Domain.identity;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.ApiControllers;
using WebApp.Controllers;
using Xunit;
using Xunit.Abstractions;
using AppUser = App.DAL.DTO.Identity.AppUser;
using Cash = App.DAL.DTO.Cash;
using Industry = App.DAL.DTO.Industry;
using Loan = App.DAL.DTO.Loan;
using Portfolio = App.DAL.DTO.Portfolio;
using Price = App.DAL.DTO.Price;
using Region = App.DAL.DTO.Region;
using Transaction = App.DAL.DTO.Transaction;

namespace Test.WebApp.Controller;

public class UnitTestPortfolioController
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AppUOW _appUow;
    private readonly PortfolioService _portfolioService;
    private readonly PortfolioRepository _portfolioRepository;
    private readonly AppDbContext _ctx;
    
    public UnitTestPortfolioController(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        
        // SetUp mock db - InMemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger<PortfoliosController>();

        var dalMapperCfg = GetDalMapperConfiguration();
        var bllMapperCfg = GetBllMapperConfiguration();

        _portfolioRepository = new PortfolioRepository(_ctx, new PortfolioMapper(new Mapper(dalMapperCfg)));
        _appUow = new AppUOW(_ctx, new Mapper(dalMapperCfg));
        
        _portfolioService = new PortfolioService(
            _portfolioRepository,
            new App.BLL.Mappers.PortfolioMapper(new Mapper(bllMapperCfg)),
            new App.Public.DTO.Mappers.PortfolioMapper(new Mapper(bllMapperCfg))
            );
    }
    
    [Fact]
    public async Task Action_GetPortfolios()
    {
        // Arrange
        _ctx.Users.Add(new App.Domain.identity.AppUser()
        {
            FirstName = "Test",
            LastName = "User",
            PasswordHash = "Tere.123",
            Email = "test@app.ee"
        });
        await _ctx.SaveChangesAsync();
        
        _portfolioService.Add(new App.Public.DTO.v1.Portfolio()
            {
                Name = "Test Portfolio Name",
                Description = "Test Portfolio Description",
            }
        );
            
        await _ctx.SaveChangesAsync();
            
        // Act
        var result = await _portfolioService.GetAll();
            
        // Assert
        Assert.NotEmpty(result);
        
        var enumerable = Enumerable.ToList(result);
        Assert.Single((IEnumerable) enumerable!);
        Assert.Equal("Test Portfolio Description", enumerable.First().Description);
    }


    [Fact]
    public async Task Action_GetByPortfolioId()
    {
        // Arrange
        _ctx.Users.Add(new App.Domain.identity.AppUser()
        {
            FirstName = "Test",
            LastName = "User",
            PasswordHash = "Tere.123",
            Email = "test@app.ee"
        });
        await _ctx.SaveChangesAsync();
        
        _portfolioService.Add(new App.Public.DTO.v1.Portfolio()
            {
                Name = "Test Portfolio 01",
                Description = "Test Portfolio Description 01",
            }
        );
        
        
        _portfolioService.Add(new App.Public.DTO.v1.Portfolio()
            {
                Name = "Test Portfolio 02",
                Description = "Test Portfolio Description 02",
            }
        );
        
        await _ctx.SaveChangesAsync();
        
            
        // Act
        var result = await _portfolioService.GetAll();
        var resultId = result.First().Id;
        var resultPortfolio = await _portfolioService.PublicFirstOrDefaultAsync(resultId);


        // Assert
        Assert.NotEmpty(result);
        Assert.Equal("Test Portfolio 01", resultPortfolio!.Name);
    }




    private static MapperConfiguration GetDalMapperConfiguration()
    {
        return new(config =>
        {
            config.CreateMap<AppUser, App.Domain.identity.AppUser>().ReverseMap();
            config.CreateMap<Portfolio, App.Domain.Portfolio>().ReverseMap();
            config.CreateMap<Loan, App.Domain.Loan>().ReverseMap();
            config.CreateMap<Cash, App.Domain.Cash>().ReverseMap();
            config.CreateMap<Industry, App.Domain.Industry>().ReverseMap();
            config.CreateMap<Price, App.Domain.Price>().ReverseMap();
            config.CreateMap<Region, App.Domain.Region>().ReverseMap();
            config.CreateMap<Transaction, App.Domain.Transaction>().ReverseMap();
        });
    }    
    
    
    private static MapperConfiguration GetBllMapperConfiguration()
        {
            return new(config =>
            {
                config.CreateMap<App.BLL.DTO.Portfolio, App.DAL.DTO.Portfolio>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Stock, App.DAL.DTO.Stock>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Loan, App.DAL.DTO.Loan>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Cash, App.DAL.DTO.Cash>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Industry, App.DAL.DTO.Industry>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Price, App.DAL.DTO.Price>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Region, App.DAL.DTO.Region>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Transaction, App.DAL.DTO.Transaction>().ReverseMap();
                
                config.CreateMap<App.Public.DTO.v1.Portfolio, App.BLL.DTO.Portfolio>().ReverseMap();
                config.CreateMap<App.Public.DTO.v1.Stock, App.BLL.DTO.Stock>().ReverseMap();
                config.CreateMap<App.Public.DTO.v1.Loan, App.BLL.DTO.Loan>().ReverseMap();
                config.CreateMap<App.Public.DTO.v1.Cash, App.BLL.DTO.Cash>().ReverseMap();
                config.CreateMap<App.Public.DTO.v1.Industry, App.BLL.DTO.Industry>().ReverseMap();
                config.CreateMap<App.Public.DTO.v1.Price, App.BLL.DTO.Price>().ReverseMap();
                config.CreateMap<App.Public.DTO.v1.Region, App.BLL.DTO.Region>().ReverseMap();
                config.CreateMap<App.Public.DTO.v1.Transaction, App.BLL.DTO.Transaction>().ReverseMap();
            });
        }

        
    
}