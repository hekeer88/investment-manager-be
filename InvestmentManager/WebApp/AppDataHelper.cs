using App.Domain;
using App.Resources.App.Domain;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using Industry = App.Domain.Industry;
using Portfolio = App.Domain.Portfolio;
using Region = App.Domain.Region;

namespace WebApp;

public static class AppDataHelper
{
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        using var serviceScope = app.
            ApplicationServices.
            GetRequiredService<IServiceScopeFactory>().
            CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("No db context");
        }
        // TODO: check database state
        //can't connect - wrong address
        //can't connect - wrong user/pass
        //can connect - but no db
        //can connect - there is db

        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }
        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }
        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            // TODO:
        }
        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            var region = new Region
            {
                Country = 
                {
                    ["et-EE"] = "Eesti",
                    ["en-GB"] = "Estonia"
                },
                Continent = 
                {
                    ["et-EE"] = "Euroopa",
                    ["en-GB"] = "Europe"
                }
            };
            
            var telecommunication = new Industry
            {
                Name = 
                {
                    ["et-EE"] = "Telekommunikatsioon",
                    ["en-GB"] = "Telecommunication"
                },
               
            };
            var finance = new Industry
            {
                Name = 
                {
                    ["et-EE"] = "Finantsteenused",
                    ["en-GB"] = "Financial Services"
                },
               
            };

            context.Industries.Add(finance);
            context.Industries.Add(telecommunication);
            context.Regions.Add(region);
            context.SaveChanges();
        }
        
    }
}