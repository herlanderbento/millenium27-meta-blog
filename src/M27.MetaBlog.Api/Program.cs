using M27.MetaBlog.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Migrations.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

//builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services
    .AddAppConections(builder.Configuration)
    .AddSecurity(builder.Configuration)
    .AddUseCases()
    
    .AddAndConfigureControllers()
    .AddHttpLogging(logging =>
    {
        logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
        logging.RequestBodyLogLimit = 4096;
        logging.ResponseBodyLogLimit = 4096;
    })
    .AddCors(p => p.AddPolicy("CORS", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));

var app = builder.Build();

app.UseHttpLogging();
app.UseDocumentation();
app.UseCors("CORS");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();

/*
using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        using var rsa = RSA.Create(2048);
        
        // Exportando a chave privada
        var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
        Console.WriteLine("Private Key:\n" + privateKey + "\n");

        // Exportando a chave pública
        var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        Console.WriteLine("Public Key:\n" + publicKey);
    }
}
*/