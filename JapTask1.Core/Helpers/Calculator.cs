using JapTask1.Common.Enums;
using JapTask1.Core.Dtos.Response;
using JapTask1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Common.Helpers
{
    public class Calculator
    {

        public static double RecipeTotalCost(GetRecipeDto recipe)
        {
            return recipe.Ingredients.Sum(i => i.Price);
        }

        //public static double RecipeTotalCost(double quantity, Units unit, double pPrice, Recipe recipe)


        //{
        //    double pricePerIng;

        //    if (unit == Units.Gr || unit == Units.Ml)
        //    {
        //        pricePerIng = (pPrice * quantity);
        //    }
        //    else
        //    {
        //        pricePerIng = (pPrice * quantity);
        //    };

        //    return pricePerIng;
        //}


        public static double PricePerIngredient(double pQuantity, Units pUnit, double pPrice, Units unit, double quantity)
        {
            double pricePerIng;

            if (unit == Units.Gr || unit == Units.Ml)
            {
                pricePerIng = (pPrice * quantity) / pQuantity / 1000;
            }
            else
            {
                pricePerIng = (pPrice * quantity) / pQuantity;
            };

            return pricePerIng;
        }
    }
}
