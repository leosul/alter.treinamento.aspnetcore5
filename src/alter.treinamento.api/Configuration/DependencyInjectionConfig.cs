using alter.treinamento.api.Extensions;
using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Interfaces.LifeTime;
using alter.treinamento.business.Models.LifeTime;
using alter.treinamento.business.Notifications;
using alter.treinamento.business.Services;
using alter.treinamento.data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace alter.treinamento.api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddTransient<ITransientService, OperationService>();
            services.AddScoped<IScopedService, OperationService>();
            services.AddSingleton<ISingletonService, OperationService>();

            //Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            //Notificator
            services.AddScoped<INotificator, Notificator>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //Identity
            services.AddScoped<IUser, User>();

            return services;
        }
    }
}
