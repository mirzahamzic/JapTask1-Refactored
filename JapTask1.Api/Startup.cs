using AutoMapper;
using JapTask1.Api.Extensions;
using JapTask1.Api.Middlewares;
using JapTask1.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JapTask1.Api
{
    public class Startup
    {
        public string ConnectionString { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpServiceConfiguration(); //adding http services

            services.AddSwaggerConfig(); //swagger config

            services.AddDBConnection(Configuration); //database config

            services.AddAutoMapper(typeof(Startup)); //automapper config

            services.AddAuthConfig(Configuration); //jwt auth settings

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
