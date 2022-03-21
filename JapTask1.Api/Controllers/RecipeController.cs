using JapTask1.Core.Dtos.Request;
using JapTask1.Core.Dtos.Response;
using JapTask1.Core.Entities;
using JapTask1.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JapTask1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRecipeDto newRecipe)
        {
            await _recipeService.Create(newRecipe);
            return Ok(newRecipe);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> Get([FromQuery] int limit)
        {
            return Ok(await _recipeService.Get(limit));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> GetById(int id)
        {
            var response = await _recipeService.GetById(id);

            if (response.Data is null)
            {
                throw new System.Exception("Recipe does not exists.");
            }
            return Ok(response);
        }

        [HttpGet, Route("searchRecipe/{searchTerm}")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeDto>>>> Search([FromQuery] string searchTerm)
        {
            return Ok(await _recipeService.Search(searchTerm));
        }
    }
}
