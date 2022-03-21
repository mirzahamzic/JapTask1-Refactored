using JapTask1.Core.Interfaces;
using JapTask1.Services.CategoryService;
using JapTask1.Services.IngredientService;
using JapTask1.Services.RecipeService;
using JapTask1.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace JapTask1.Api.Extensions
{
    public static class HttpServicesConfigurationExtension
    {
        public static void AddHttpServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
