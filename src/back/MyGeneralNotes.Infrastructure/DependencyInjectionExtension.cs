using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyGeneralNotes.Domain.Repositories;
using MyGeneralNotes.Domain.Repositories.Routine;
using MyGeneralNotes.Domain.Repositories.User;
using MyGeneralNotes.Domain.Security.Cryptography;
using MyGeneralNotes.Domain.Security.Tokens;
using MyGeneralNotes.Domain.Services.LoggedUser;
using MyGeneralNotes.Infrastructure.DataAccess;
using MyGeneralNotes.Infrastructure.DataAccess.Repositories;
using MyGeneralNotes.Infrastructure.Extensions;
using MyGeneralNotes.Infrastructure.Security.Cryptography;
using MyGeneralNotes.Infrastructure.Security.Tokens.Acces.Generator;
using MyGeneralNotes.Infrastructure.Security.Tokens.Acces.Validator;
using MyGeneralNotes.Infrastructure.Services.LoggedUser;
using System.Reflection;

namespace MyGeneralNotes.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPasswordEncrypter(services, configuration);
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddMigrations(services, configuration);
        AddTokens(services, configuration);
        AddLoggedUser(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));

        services.AddDbContext<MyGeneralNotesDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserWriteOnlyRepository, UserRepository>()
                .AddScoped<IUserReadOnlyRepository, UserRepository>()
                .AddScoped<IUserUpdateOnlyRepository, UserRepository>();

        services.AddScoped<IRoutineWriteOnlyRepository, RoutineRepository>()
                .AddScoped<IRoutineReadOnlyRepository, RoutineRepository>()
                .AddScoped<IRoutineUpdateOnlyRepository, RoutineRepository>();
    }

    private static void AddMigrations(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        services.AddFluentMigratorCore().ConfigureRunner(opt =>
        {
            opt
            .AddMySql5()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("MyGeneralNotes.Infrastructure")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var tokenLifeTime = uint.Parse(configuration.GetSection("Settings:Jwt:TokenLifeTime").Value!);
        var sigininKey = configuration.GetSection("Settings:Jwt:SigninKey").Value!;

        services.AddScoped<IAccessTokenGenerator>(opt => new JwtTokenGenerator(tokenLifeTime, sigininKey!));
        services.AddScoped<IAccessTokenValidator>(opt => new JwtTokenValidator(sigininKey!));
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var additionalKey = configuration.GetSection("Settings:Password:AdditionalKey").Value;
        services.AddScoped<IPasswordEncripter>(opt => new Sha512Encripter(additionalKey!));
    }
}
