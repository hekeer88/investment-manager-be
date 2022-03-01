using App.Domain;
using App.Domain.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data;

// generics to use own Users and Roles
public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid> 
{
    
    public DbSet<Cash> Cashes { get; set; } = default!;
    public DbSet<Industry> Industries { get; set; } = default!;
    public DbSet<Loan> Loans { get; set; } = default!;
    public DbSet<Portfolio> Portfolios { get; set; } = default!;
    public DbSet<Price> Prices { get; set; } = default!;
    public DbSet<Region> Regions { get; set; } = default!;
    public DbSet<Stock> Stocks { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    
    
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Remove cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
    }
    
    
}