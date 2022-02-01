using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Matrix.AppSample.HttpApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Matrix.AppSample.HttpApi.Infrastructure
{
    internal static class ServicesExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder
                            .SetIsOriginAllowed((host) => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(configuration.GetValue("AllowedOrigins", "*")));
            });
            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.Authority = configuration["Auth:Authorite"];
                 x.Audience = configuration["Auth:Audience"];
             });

            return services;
        }

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            //services.AddScoped<IApponintmentQueries, ApponintmentQueries>();
            return services;
        }
    }
}
