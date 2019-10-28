using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Context;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             *              #####   INYECCION DE DEPENDENCIAS: TIEMPO DE VIDA   #####
             *              
             * Transient: Cada vez que un servicio sea solicitado, se va a servir una nueva instancia de la clase
             *            ## services.AddTransient<IClaseB, ClaseB>(); ##
             * 
             * Scoped: Se crea una instancia del servicio por cada solicitud Http diferente. Por ej.: Si varios
             *         varios clientes hacen una peticion GET, se les entregara la misma instancia, en cambio si
             *         un nuevo cliente realiza unapeticion POST, se le entregara una nueva instancia.
             *         ## services.AddScoped<IClaseB, ClaseB>(); ##
             * 
             * Singleton: Se entregara siempre la misma instancia del servicio, a menos que el servidor sea
             *            apagado y encendido nuevamente.
             *            ## services.AddSingleton<IClaseB, ClaseB>(); ##
             *            
             *            
             * IMPORTANTE: Si un servicio depende de DbContext, este servicio debe ser servido utilizando AddScoped
             
             */

            /* Inicio : Configuracion AutoMapper */
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            /* Fin : Configuracion AutoMapper */

            services.AddScoped<IAutoresService, AutoresService>();
            services.AddScoped<ILibrosService, LibrosService>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
