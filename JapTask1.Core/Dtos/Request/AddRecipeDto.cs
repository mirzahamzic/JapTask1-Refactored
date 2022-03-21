using JapTask1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Dtos.Request
{
    public class AddRecipeDto : RecipeDto
    {
        public List<AddRecipeIngredientDto> AddRecipeIngredientDto { get; set; }
    }
}
