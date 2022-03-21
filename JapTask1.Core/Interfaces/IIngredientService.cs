using JapTask1.Core.Dtos;
using JapTask1.Core.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Interfaces
{
    public interface IIngredientService
    {
        public Task<List<IngredientDto>> Get();
    }
}
