using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarienProyect.Solicitud.Aplication;
using DarienProyect.Solicitud.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Unity;

namespace DarienProyect.Solicitud.Distribution.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureContainer(IUnityContainer container)
        {

            container.AddContainerDataSolicitud();
            container.AddContainerAplicationSolicitud();

        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAplicationSolicitud();
            services.AddControllers();

            services.AddDbContext<SolicitudDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocalConnection"),

                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly("DarienProyect.Solicitud.Distribution.Api");

                    });

            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
