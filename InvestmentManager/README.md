#Investment Manager

Database migrations and update
~~~sh
dotnet ef migrations add --project App.DAL.EF --startup-project WebApp Initial
dotnet ef migrations remove --project App.DAL.EF --startup-project WebApp --context AppDbContext
dotnet ef database update --project App.DAL.EF --startup-project WebApp
dotnet ef database drop --project App.DAL.EF --startup-project WebApp
~~~


Web Controllers
keep this controllers for testing what is db for example
~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name PortfoliosController   -actions -m App.Domain.Portfolio   -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CashesController   -actions -m App.Domain.Cash   -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name IndustriesController   -actions -m App.Domain.Industry   -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name LoansController   -actions -m App.Domain.Loan   -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PricesController   -actions -m App.Domain.Price   -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RegionsController   -actions -m App.Domain.Region   -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StocksController   -actions -m App.Domain.Stock   -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TransactionsController   -actions -m App.Domain.Transaction   -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

WebApi Controllers
~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name PortfoliosController   -actions -m App.Domain.Portfolio   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name CashesController   -actions -m App.Domain.Cash   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name IndustriesController   -actions -m App.Domain.Industry   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name LoansController   -actions -m App.Domain.Loan   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name PricesController   -actions -m App.Domain.Price   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name RegionsController   -actions -m App.Domain.Region   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name StocksController   -actions -m App.Domain.Stock   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TransactionsController   -actions -m App.Domain.Transaction   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~

c# kasutasin seda rezoirpage jaoks
~~~sh
dotnet aspnet-codegenerator razorpage -m Job -dc AppDbContext -udl -outDir Pages/Jobs --referenceScriptLibraries -f
~~~



Docker build and run
~~~sh
docker build -t docker-test-aspnet-6 .
docker run --name webapp_docker --rm -it -p 8000:80 docker-test-aspnet-6
docker tag docker-test-aspnet-6 henri88/docker-test-aspnet-6:latest
docker login -u henri88
docker push henri88/docker-test-aspnet-6:latest
~~~

Update:
kui webhook on lisatud(hetkel on), siis peale muudatust build -> tag(as latest again) -> push
