#Investment Manager

Database migrations and update
~~~sh
dotnet ef migrations add --project DAL.App --startup-project WebApp Initial
dotnet ef database update --project DAL.App --startup-project WebApp
~~~


Web Controllers
~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name PortfoliosController   -actions -m App.Domain.Portfolio   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CashesController   -actions -m App.Domain.Cash   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name IndustriesController   -actions -m App.Domain.Industry   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LoansController   -actions -m App.Domain.Loan   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PricesController   -actions -m App.Domain.Price   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StocksController   -actions -m App.Domain.Stock   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TransacionsController   -actions -m App.Domain.Transactions   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

dotnet aspnet-codegenerator razorpage -m Job -dc AppDbContext -udl -outDir Pages/Jobs --referenceScriptLibraries -f