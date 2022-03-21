using JapTask1.Core.Dtos.Request;
using JapTask1.Core.Dtos.Response;
using JapTask1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Interfaces
{
    public interface IRecipeService
    {
        Task<ServiceResponse<List<GetRecipeDto>>> Get(int limit);
        Task<ServiceResponse<List<GetRecipeDto>>> GetByCategory(int categoryId, int limit);
        Task<ServiceResponse<GetRecipeDto>> GetById(int recipeId);
        Task Create(AddRecipeDto recipe);
        Task<ServiceResponse<List<GetRecipeDto>>> Search(string searchTerm);

    }
}
