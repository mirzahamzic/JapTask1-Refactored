using AutoMapper;
using JapTask1.Api.Extensions;
using JapTask1.Api.Middlewares;
using JapTask1.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapTask1.Api
{
    public class Startup
    {
        public string ConnectionString { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
            configuration.GetSection("AppSettings:Token").Bind(AuthenticationConfigurationExtension.Setting);

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpServiceConfiguration(); //adding http services

            services.AddSwaggerConfig(); //swagger config

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString)); //database config

            services.AddAutoMapper(typeof(Startup)); //automapper config

            services.AddAuthConfig(); //jwt auth settings

            services.AddCorsConfiguration(); //add cors

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfig();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); //use authentication

            app.UseRouting();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware)); //exception middleware

            app.UseCors("CORS"); //use cors

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DatabaseSeed.Seed(app); //seeding the database upon first run of the app

        }
    }
}
