using JapTask1.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Dtos.Response
{
    public class GetIngredientDto
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public Units Unit { get; set; }
        public double Price { get; set; }
    }
}
