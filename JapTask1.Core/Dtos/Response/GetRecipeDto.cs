using JapTask1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Dtos.Response
{
    public class GetRecipeDto : RecipeDto
    {
        public int Id { get; set; }
        public double TotalCost { get; set; } // { get { return Ingredients.Sum(i => i.Price); } }
        public List<GetIngredientDto> Ingredients { get; set; }

    }
}
