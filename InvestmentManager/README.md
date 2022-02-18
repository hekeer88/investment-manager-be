#Investment Manager

Database migrations and update
~~~sh
dotnet ef migrations add --project DAL.App --startup-project WebApp Initial
dotnet ef database update --project DAL.App --startup-project WebApp
~~~


Web Controllers
~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name ListItemsController - actions -m Domain.Listitem -dc ApplicationDbContext -outDir Areas/Admin/Controllers --usDefaultLayout -
~~~

