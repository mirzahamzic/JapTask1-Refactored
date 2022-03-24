using JapTask1.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public double PurchasedQuantity { get; set; }
        public double PurchasedPrice { get; set; }
        public Units PurchasedUnitOfMeasure { get; set; }


        //many-to-many between ingredients and recipes
        public List<RecipeIngredient> RecipesIngredients { get; set; }
    }
}
