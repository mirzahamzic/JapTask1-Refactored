using JapTask1.Common.Enums;
using JapTask1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Dtos
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PurchasedQuantity { get; set; }
        public double PurchasedPrice { get; set; }
        public Units PurchasedUnitOfMeasure { get; set; }
    }
}
