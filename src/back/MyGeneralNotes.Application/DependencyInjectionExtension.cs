using Microsoft.Extensions.DependencyInjection;
using MyGeneralNotes.Application.Services.AutoMapper;
using MyGeneralNotes.Application.UseCases.Dashboard;
using MyGeneralNotes.Application.UseCases.Login.DoLogin;
using MyGeneralNotes.Application.UseCases.Routines.Delete;
using MyGeneralNotes.Application.UseCases.Routines.GetById;
using MyGeneralNotes.Application.UseCases.Routines.Register;
using MyGeneralNotes.Application.UseCases.Routines.Update;
using MyGeneralNotes.Application.UseCases.User.ChangePassword;
using MyGeneralNotes.Application.UseCases.User.Profile;
using MyGeneralNotes.Application.UseCases.User.Register;
using MyGeneralNotes.Application.UseCases.User.Update;

namespace MyGeneralNotes.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }
    private static void AddAutoMapper(IServiceCollection services)
    {

        services.AddScoped(opt => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new MappingConfig());
        }).CreateMapper());
    }
    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();

        services.AddScoped<IDashboardUseCase, DashboardUseCase>();

        services.AddScoped<IRegisterRoutineUseCase, RegisterRoutineUseCase>();
        services.AddScoped<IGetRoutineByIdUseCase, GetRoutineByIdUseCase>();
        services.AddScoped<IUpdateRoutineUseCase, UpdateRoutineUseCase>();
        services.AddScoped<IDeleteRoutineUseCase, DeleteRoutineUseCase>();
    }
}
