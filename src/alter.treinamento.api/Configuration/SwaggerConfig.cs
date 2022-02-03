using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace alter.treinamento.api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.OperationFilter<SwaggerDefaultValues>();
                c.DocumentFilter<SwaggerXLogo>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter the JWT token like this: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "swagger";
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            foreach (var description in provider.ApiVersionDescriptions)
            {
                app.UseReDoc(s =>
                {
                    s.DocumentTitle = $"API Documentation {description.GroupName}";
                    s.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
                    s.RoutePrefix = $"swagger-{description.GroupName}";
                });
            }

            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

        }
        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "API - alter-solutions.com",
                Version = description.ApiVersion.ToString(),
                Description = "Esta é uma API construída para treinamento.",
                Contact = new OpenApiContact() { Name = "Leonardo Jacques da Silva", Email = "lsilva@alter-solutions.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão está obsoleta!";
            }

            return info;
        }
    }
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription
                    .ParameterDescriptions
                    .First(p => p.Name == parameter.Name);

                var routeInfo = description.RouteInfo;

                operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null)
                {
                    parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue.ToString());
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }

    public class SwaggerXLogo : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!swaggerDoc.Info.Extensions.ContainsKey("x-logo"))
                swaggerDoc.Info.Extensions.Add("x-logo", new OpenApiObject
            {
                {"url", new OpenApiString("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAt1BMVEX////uOSPyaCLtIiTsAAD97evtHwDuMxr//Pr0f0byZR3xWADybV8AAAD60cT5yMTxXgD5xMU4OTnAwMD4+Pjg4OB0dXUbHh4+Pz/xYWKVlpZra2vV1dX+9e/zdjrt7e1LTEz2opqur68tLy+bm5vLy8sKDQ0mJycwMjKlpaUVFxeKi4taW1v60s/xZVT6zb/wTABYWVl9fn7wSEr9zM30fn/4s6zwUDy6urpHSEj1kmLzcjH/4dLpYlNOAAADSElEQVR4nO3d3XKbRhiA4W3quG7dz07EYhHAXioE6Ac7kZqkKMn9X1e+XXDaTGZ8uEzj9/GMZUnLzwsLmvGBbQwAAAAAAAAAAAAAAPgfe302n9eRCn+dC4UUUkghhRRSSCGFFFJIIYUUUkghhRRSSCGFkQt/9t95AwAAAAAAAACei4uX87mIUnh9dj6Xs+s4hee/zOWcQgoppJBCCimkkEIKKaSQQgoppJBCCimkkMK4hb/NJVLh/cOfc3m4j1IIAAAAAAAAAHg2LuYTJ/Dq7ve53F3FKbz8Yy6XkQpfvZjLKwoppJBCCimkkEIKKaSQQgoppJBCCimkkEIKYxa+fXc5l3dvoxQCAAAAiKWX3j+ksrDGWBkV5u/pX2u9T1t93h4afXPv30l1cFnpT0c/PkvCStLO+YftjTk+riDpzLRIrcuaYRlGHNfGNLVIt4oW6HQXylBYhz3OFtJmi6P58GZ0X4pUVecH5ZU1NtekQXT/3FGcsYupcPlYqPLCf092fnWFvp744SvJdP3mJjeu02Uaf3zi2MtJjqFw47dpbSOZs//ZfKnp1haSmz6fDko3hMdel3u6MK/M+KMzq+LGzxUtbGoXoesbK23Z6cl4LPRF4WB//Gv0Ugv9nmnhILnzbyXZOLLp7NOFj3PYbAYttNU+FNqsT6KdQGPWuud7Wf9Y+M/Z9Mdwy7YtilOYyk0vsrdmOIzLpnpknix0m3IcqWd/VegFkfhC4/JW/HUdhdOrTr/kx8L769Ft6e80Uk/7mh4q00wjk/rfc9ilofD4XaHJxuls9MrTQt1GeZxm+lbKOIUrqauT3kdW/k6TOqezcCq8nejzzpbS6jEPpykVY+ttWLjKv91p7NhyWH9fuO7GrXQuFJphc1obG9ZzGOIUnmTQ28ggJ5vqearr5VaL/MeG+fT5zvv8RQv9ZNZrri/0GBS6p43snSv7k95aFys9LKnVQ9U4l0s4ufuxsNXwPiud2+rsHAtNrhdEstFlkmWcc1h21Ri6K91uMxamuz4U3k2F6eKkT4vlXq+fZRs+BU1aSLtc2/D6breTrf+8E+nHm+Q6zNXGL2a2+vLBz+AhvGhudGhzkHYR6zoEAAAAAAAAAAAA8HP6CuVYxY3BpBO0AAAAAElFTkSuQmCC")},
                {"backgroundColor", new OpenApiString("#FFFFFF")},
                {"altText", new OpenApiString("Company Logo")}
            });
        }
    }
    public class SwaggerAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerAuthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/docs")
                && !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }
    }
}
