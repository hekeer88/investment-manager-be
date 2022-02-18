using App.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data;

public class AppDbContext : IdentityDbContext
{
    public DbSet<Cash> Cashes { get; set; } = default!;
    public DbSet<Industry> Industries { get; set; } = default!;
    public DbSet<Loan> Loans { get; set; } = default!;
    public DbSet<Portfolio> Portfolios { get; set; } = default!;
    public DbSet<Price> Prices { get; set; } = default!;
    public DbSet<Stock> Stocks { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    { 
        base.OnModelCreating(builder);

        builder.Entity<Stock>()
            .HasOne(s => s.Portfolio)
            .WithMany(p => p.Stocks);

    }
    
    
    
    
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}