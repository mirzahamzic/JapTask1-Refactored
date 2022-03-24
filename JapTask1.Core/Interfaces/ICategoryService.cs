using JapTask1.Core.Dtos.Request;
using JapTask1.Core.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<GetCategoryDto>> Get(CategorySearch req);

    }
}
