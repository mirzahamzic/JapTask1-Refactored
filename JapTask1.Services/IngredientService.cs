using AutoMapper;
using JapTask1.Core.Dtos;
using JapTask1.Core.Dtos.Response;
using JapTask1.Core.Interfaces;
using JapTask1.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Services.IngredientService
{
    public class IngredientService : IIngredientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public IngredientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<IngredientDto>> Get()
        {
            var dbIngredients = await _context.Ingredients.Select(i => _mapper.Map<IngredientDto>(i)).ToListAsync();
            return dbIngredients;
        }
    }
}
