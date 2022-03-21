using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Dtos.Response
{
    public class GetRecipeTotalCostDto
    {
        public double TotalCost { get; set; }

        public GetIngredientDto Ingredients { get; set; }
    }
}
