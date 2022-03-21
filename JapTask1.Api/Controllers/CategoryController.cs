using JapTask1.Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace JapTask1.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORS")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getCategories")]
        public async Task<IActionResult> GetAllCategories([FromQuery][Optional] string limit)
        {
            var allCategories = await _categoryService.Get(limit);
            return Ok(allCategories);
        }
    }
}
