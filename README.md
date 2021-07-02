# projeto_core_backend

Projeto Livre - WebAPI com Asp.NET Core 3.1

#### Instruções
    1- dotnet restore
    2- dotnet add package Microsoft.AspNetCore.Authentication 2.2.0
    3- dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer 3.1.5
    4- dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 3.1.5
    5- dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.1.5
    6- dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.0.1
    7- dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.7
    8- dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 3.1.7
    9- dotnet add package Swashbuckle.AspNetCore --version 5.5.1
    10- Configuração de banco de dados em appsettings.json
    11- Executar: dotnet ef migrations add InitialCreate --project ProjetoCoreWebAPI
    12- Executar: dotnet ef database update --project ProjetoCoreWebAPI
    14- Executar: F5 ou ctrl +F5
