# grab the sdk image, create an name for it - "build"
FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.props .

# copy ALL the projects
COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.Contracts.BLL/*.csproj ./App.Contracts.BLL/
COPY App.Contracts.DAL/*.csproj ./App.Contracts.DAL/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.Public.DTO/*.csproj ./App.Public.DTO/
COPY WebApp/*.csproj ./WebApp/

COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.DAL/*.csproj ./Base.DAL/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Extensions/*.csproj ./Base.Extensions/

COPY Base.Contracts.Base/*.csproj ./Base.Contracts.Base/
COPY Base.Contracts.BLL/*.csproj ./Base.Contracts.BLL/
COPY Base.Contracts.DAL/*.csproj ./Base.Contracts.DAL/
COPY Base.Contracts.Domain/*.csproj ./Base.Contracts.Domain/

COPY App.Resources/*.csproj ./App.Resources/
COPY Test.WebApp/*.csproj ./Test.WebApp/
  
# create the first layer with just niget packages installed
RUN dotnet restore


# copy everything else and build app
COPY App.BLL/. ./App.BLL/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.Contracts.BLL/. ./App.Contracts.BLL/
COPY App.Contracts.DAL/. ./App.Contracts.DAL/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY App.Domain/. ./App.Domain/
COPY App.Public.DTO/. ./App.Public.DTO/
COPY WebApp/. ./WebApp/
#COPY WebApp/appsettings.docker.json ./WebApp/appsettings.json

COPY Base.BLL/. ./Base.BLL/
COPY Base.DAL/. ./Base.DAL/
COPY Base.DAL.EF/. ./Base.DAL.EF/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Extensions/. ./Base.Extensions/

COPY Base.Contracts.Base/. ./Base.Contracts.Base/
COPY Base.Contracts.BLL/. ./Base.Contracts.BLL/
COPY Base.Contracts.DAL/. ./Base.Contracts.DAL/
COPY Base.Contracts.Domain/. ./Base.Contracts.Domain/

COPY App.Resources/. ./App.Resources/
COPY Test.WebApp/. ./Test.WebApp/

WORKDIR /app/WebApp

# compile the app with Release option and put files into dir "out"
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime
WORKDIR /app
# copy files from previous image ("build")
COPY --from=build /app/WebApp/out ./
# run this command when container starts up
ENTRYPOINT ["dotnet", "WebApp.dll"]

