using Microsoft.Extensions.DependencyInjection;

namespace JapTask1.Api.Extensions
{
    public static class CorsConfigurationExtension
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }
    }
}
