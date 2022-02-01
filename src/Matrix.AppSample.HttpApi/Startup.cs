using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Matrix.AppSample.HttpApi.Infrastructure;
using Matrix.AppSample.HttpApi.Infrastructure.AutofacModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Matrix.AppSample.HttpApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //TODO : Adicionar serviço para observabilidade (app insights, elastic, datadog, etc)
            services
                .AddHttpClients()
                .AddQueries()
                .AddAuth(Configuration)
                .AddCustomMvc(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new InfrastructureModule());
            container.RegisterModule(new AplicacaoModule());
            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrWhiteSpace(pathBase))
            {
                loggerFactory
                    .CreateLogger<Startup>()
                    .LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // TODO: Configurar endpoint de healthcheck e liveness
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}