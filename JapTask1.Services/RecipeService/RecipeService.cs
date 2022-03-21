using AutoMapper;
using JapTask1.Core.Dtos.Request;
using JapTask1.Core.Dtos.Response;
using JapTask1.Core.Entities;
using JapTask1.Core.Interfaces;
using JapTask1.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace JapTask1.Services.RecipeService
{
    public class RecipeService : IRecipeService

    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeService(AppDbContext context, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        //getting user id from token
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task Create(AddRecipeDto recipe)
        {

            var newRecipe = new Recipe()
            {
                Name = recipe.Name,
                Description = recipe.Description,
                CategoryId = recipe.CategoryId,
                CreatedAt = DateTime.Now,
                //UserId = user.Id,
            };

            await _context.Recipes.AddAsync(newRecipe);
            await _context.SaveChangesAsync();

            var ingredientsToSave = new List<RecipeIngredient>();

            foreach (var ingredient in recipe.AddRecipeIngredientDto)
            {
                ingredientsToSave.Add(new RecipeIngredient()
                {
                    RecipeId = newRecipe.Id,
                    IngredientId = ingredient.IngredientId,
                    Unit = ingredient.Unit,
                    Quantity = ingredient.Quantity,
                });
            }
            await _context.RecipesIngredients.AddRangeAsync(ingredientsToSave);
            await _context.SaveChangesAsync();
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> Get(int limit)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();

            int pageSize;
            pageSize = Int16.Parse(_configuration.GetSection("Pagination:Limit").Value);

            var dbRecipes = await _context.Recipes
            .Include(r => r.Category)
            .Include(r => r.RecipesIngredients)
            .ThenInclude(i => i.Ingredient)
            .ToListAsync();

            serviceResponse.Data = dbRecipes.Select(recipe => _mapper.Map<GetRecipeDto>(recipe)).Skip(limit).Take(pageSize).ToList();


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> GetByCategory(int categoryId, int limit)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();

            int pageSize;
            pageSize = Int16.Parse(_configuration.GetSection("Pagination:Limit").Value);

            var dbRecipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipesIngredients)
                .ThenInclude(i => i.Ingredient)
                .Where(r => r.CategoryId == categoryId)
                .ToListAsync();

            serviceResponse.Data = dbRecipes.Select(recipe => _mapper.Map<GetRecipeDto>(recipe)).Skip(limit).Take(pageSize).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRecipeDto>> GetById(int recipeId)
        {
            var serviceResponse = new ServiceResponse<GetRecipeDto>();

            var dbRecipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipesIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            serviceResponse.Data = _mapper.Map<GetRecipeDto>(dbRecipes);
            //serviceResponse.Data = dbRecipes.RecipesIngredients.Select(i => _mapper.Map<GetRecipeDto>(i));

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> Search(string searchTerm)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeDto>>();

            var dbRecipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipesIngredients)
                .ThenInclude(i => i.Ingredient)
                .Where(n => n.Name.ToLower().Contains(searchTerm) || n.Description.ToLower().Contains(searchTerm))
                .ToListAsync();

            serviceResponse.Data = dbRecipes.Select(recipe => _mapper.Map<GetRecipeDto>(recipe)).ToList();

            return serviceResponse;
        }
    }
}



