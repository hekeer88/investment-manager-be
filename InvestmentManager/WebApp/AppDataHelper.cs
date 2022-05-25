using System.Security.Claims;
using App.DAL.EF;
using App.Domain;
using App.Domain.identity;
using App.Resources.App.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        
        if (context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory") return;
        
        
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
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            if (userManager == null || roleManager == null)
            {
                throw new NullReferenceException("userManager or roleManager cannot be null!");
            }

            var roles = new (string name, string displayName)[]
            {
                ("admin", "System administrator"),
                ("user", "Normal system user")
            };

            foreach (var roleInfo in roles)
            {
                var role = roleManager.FindByNameAsync(roleInfo.name).Result;
                if (role == null)
                {
                    var identityResult = roleManager.CreateAsync(new AppRole()
                    {
                        Name = roleInfo.name,
                        DisplayName = roleInfo.displayName
                    }).Result;
                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed");
                    }
                }
            }

            var users = new (string username, string firstName,string lastName, string password, string roles)[]
            {
                ("admin@app.ee","Admin","Admin", "Tere.123", "user,admin"),
                ("henri@app.ee","Henri","Keerutaja", "Tere.123", "user,admin"),
                ("user@app.ee","User","User", "Tere.123", "user"),
                ("user2@app.ee","User No Roles","User2", "Tere.123", ""),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.username).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.username,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        UserName = userInfo.username,
                        EmailConfirmed = true,
                    };
                    var identityResult = userManager.CreateAsync(user, userInfo.password).Result;
                    identityResult =  userManager.AddClaimAsync(user, new Claim("aspnet.firstname",user.FirstName)).Result;
                    identityResult =  userManager.AddClaimAsync(user, new Claim("aspnet.lastname",user.LastName)).Result;

                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Cannot create user!");
                    }
                }

                if (!string.IsNullOrWhiteSpace(userInfo.roles))
                {
                    var identityResultRole = userManager.AddToRolesAsync(user,
                        userInfo.roles.Split(",").Select(r => r.Trim())
                    ).Result;
                }
            }
        }
        
        
        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            var regions = context.Regions.ToListAsync().Result;
            if (regions.Count == 0)
            {
                var regionEST = new Region
                {
                    Country = "Estonia",
                    Continent = "Europe"
                };
            
                var regionLAV = new Region
                {
                    Country = "LAV",
                    Continent = "Africa"
                };
                var regionFIN = new Region
                {
                    Country = "Finland",
                    Continent = "Europe"
                };
                var regionNY = new Region
                {
                    Country = "New York",
                    Continent = "America"
                };
                var regionCH = new Region
                {
                    Country = "China",
                    Continent = "Asia"
                };
                context.Regions.Add(regionEST);
                context.Regions.Add(regionLAV);
                context.Regions.Add(regionFIN);
                context.Regions.Add(regionNY);
                context.Regions.Add(regionEST);
                context.Regions.Add(regionCH);
                context.SaveChanges();
            } 
            
            
            var industries = context.Industries.ToListAsync().Result;
            if (industries.Count == 0)
            {
                var telecommunication = new Industry
                {
                    Name = "Telecommunication"
                };
                var finance = new Industry
                {
                    Name = "Finance"
                };
                var energy = new Industry
                {
                    Name = "Energy"
                };
                var health = new Industry
                {
                    Name = "Health Care"
                };
                var re = new Industry
                {
                    Name = "Real Estate"
                };

                context.Industries.Add(finance);
                context.Industries.Add(telecommunication);
                context.Industries.Add(energy);
                context.Industries.Add(health);
                context.Industries.Add(re);
                
                context.SaveChanges();
            }
        }
        
    }
}