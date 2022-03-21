using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Entities
{
    public class Category : BaseEntity
    {
        public List<Recipe> Recipes { get; set; }
    }
}
