using FluentValidation;
using Iris.Validators;
using IrisCore.DTOs;
using IrisCore.InterfacesRepositories;
using IrisCore.Services.Implementations;
using IrisCore.Services.Interfaces;
using IrisInfraestructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Iris.Extensions
{
    internal static class DependencyInjectionExtension
    {
        internal static void AddDependencies(this IServiceCollection services, WebApplicationBuilder? webApplication)
        {
            ArgumentNullException.ThrowIfNull(webApplication, nameof(WebApplicationBuilder));

            //Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITaskService, TaskService>();

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITaskRepository, TaskRepository>();

            //Validators
            services.AddScoped<IValidator<TaskRequestDTO>, ToDoRequestDTOValidator>();
            services.AddScoped<IValidator<UserLoginDTO>, UserLoginDTOValidator>();
        }
    }

}
