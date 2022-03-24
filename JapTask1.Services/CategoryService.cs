using AutoMapper;
using JapTask1.Core.Dtos.Request;
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

        public async Task<List<GetCategoryDto>> Get([Optional] CategorySearch req)
        {
            //var req = new CategorySearch();
            int pageSize;
            pageSize = Int16.Parse(_configuration.GetSection("Pagination:Limit").Value);

            var query = _context.Categories.AsQueryable();

            //if (req.Id != null)
            //{
            //    query = query.Where(x => x.Id == req.Id).AsQueryable();
            //}

            if (req.Name != null)
            {
                query = query
                    .Where(x => x.Name.ToLower().Contains(req.Name.ToLower()))
                    .AsQueryable();
            }

            if (req.Limit != null)
            {
                query = query
                .Skip((int)req.Limit)
                .Take(pageSize)
                .AsQueryable();
            }

            return await query
                .Select(c => _mapper.Map<GetCategoryDto>(c))
                .ToListAsync();
        }
    }
}
