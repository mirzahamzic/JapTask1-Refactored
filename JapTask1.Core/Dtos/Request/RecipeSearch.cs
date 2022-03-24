using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Dtos.Request
{
    public class RecipeSearch : BaseSearch
    {
        public string SearchTerm { get; set; }
    }
}
