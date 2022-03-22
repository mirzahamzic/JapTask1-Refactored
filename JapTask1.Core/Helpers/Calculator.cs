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

        public static double RecipeTotalCost(Recipe recipe)
        {
            double totalCost = 0;

            foreach (var item in recipe.RecipesIngredients)
            {
                if (item.Unit == Units.Gr || item.Unit == Units.Ml)
                {
                    totalCost += ((item.Quantity * item.Ingredient.PurchasedPrice) / item.Ingredient.PurchasedQuantity / 1000);
                }
                else
                {
                    totalCost += ((item.Quantity * item.Ingredient.PurchasedPrice) / item.Ingredient.PurchasedQuantity);

                }
            }
            return totalCost;
        }


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
