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
using System.Runtime.InteropServices;
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
            var user = _context.Users.FirstOrDefault(u => u.Id == GetUserId());

            var newRecipe = new Recipe()
            {
                Name = recipe.Name,
                Description = recipe.Description,
                CategoryId = recipe.CategoryId,
                CreatedAt = DateTime.Now,
                UserId = user.Id,
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

        public async Task<ServiceResponse<List<GetRecipeDto>>> Get([Optional] BaseSearch req)
        {
            int pageSize;
            pageSize = Int16.Parse(_configuration.GetSection("Pagination:Limit").Value);

            var query = _context.Recipes
                    .Include(r => r.Category)
                    .Include(r => r.RecipesIngredients)
                    .ThenInclude(i => i.Ingredient)
                    .Where(r => r.User.Id == GetUserId())
                    .AsQueryable();

            if (req.Limit != null)
            {
                query = query
                    .Skip((int)req.Limit)
                    .Take(pageSize)
                    .AsQueryable();
            }


            var dbRecipes = await query
                    .Select(r => _mapper.Map<GetRecipeDto>(r))
                    .ToListAsync();

            return new ServiceResponse<List<GetRecipeDto>>()
            {
                Data = dbRecipes
            };
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> GetByCategory(int categoryId, [Optional] BaseSearch req)
        {
            int pageSize;
            pageSize = Int16.Parse(_configuration.GetSection("Pagination:Limit").Value);

            var query = _context.Recipes
                     .Include(r => r.Category)
                     .Include(r => r.RecipesIngredients)
                     .ThenInclude(i => i.Ingredient)
                     .Where(r => r.CategoryId == categoryId && r.User.Id == GetUserId())
                     .AsQueryable();

            if (req.Limit != null)
            {
                query = query
                    .Skip((int)req.Limit)
                    .Take(pageSize)
                    .AsQueryable();
            }

            var dbRecipes = await query
                    .Select(r => _mapper.Map<GetRecipeDto>(r))
                    .ToListAsync();

            return new ServiceResponse<List<GetRecipeDto>>()
            {
                Data = dbRecipes
            };
        }

        public async Task<ServiceResponse<GetRecipeDto>> GetById(int recipeId)
        {
            var dbRecipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipesIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == recipeId && r.UserId == GetUserId());

            return new ServiceResponse<GetRecipeDto>()
            {
                Data = _mapper.Map<GetRecipeDto>(dbRecipes)
            };
        }

        public async Task<ServiceResponse<List<GetRecipeDto>>> Search([Optional] RecipeSearch req)
        {
            int pageSize;
            pageSize = Int16.Parse(_configuration.GetSection("Pagination:Limit").Value);

            var query = _context.Recipes
                 .Include(r => r.Category)
                 .Include(r => r.RecipesIngredients)
                 .ThenInclude(i => i.Ingredient)
                 .Where(r => (r.Name.ToLower().Contains(req.SearchTerm) || r.Description.ToLower().Contains(req.SearchTerm)) && r.UserId == GetUserId())
                 .AsQueryable();

            if (req.Limit != null)
            {
                query = query
                    .Skip((int)req.Limit)
                    .Take(pageSize)
                    .AsQueryable();
            }

            var dbRecipes = await query
                     .Select(recipe => _mapper.Map<GetRecipeDto>(recipe))
                     .ToListAsync();

            return new ServiceResponse<List<GetRecipeDto>>()
            {
                Data = dbRecipes
            };
        }
    }
}



