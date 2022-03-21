using AutoMapper;
using JapTask1.Core.Dtos.Response;
using JapTask1.Core.Interfaces;
using JapTask1.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<GetCategoryDto>> Get([Optional] string limit)
        {
            int pageSize;
            pageSize = Int16.Parse(_configuration.GetSection("Pagination:Limit").Value);

            var dbCategories = await _context.Categories.ToListAsync();

            if (limit == null)
            {
                return dbCategories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
                //.OrderBy(c => c.Name)

            }
            else
            {
                return dbCategories.Select(c => _mapper.Map<GetCategoryDto>(c))
                //.OrderBy(c => c.Name)
                .Skip(Int16.Parse(limit))
                .Take(pageSize)
                .ToList();
            }
        }
    }
}
