using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //Navigation properties

        //one-to-many between category and recipe
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //one-to-many between user and recipe
        public int? UserId { get; set; }
        public User User { get; set; }

        //many-to-many between recipes and ingredients
        public List<RecipeIngredient> RecipesIngredients { get; set; }
    }
}

