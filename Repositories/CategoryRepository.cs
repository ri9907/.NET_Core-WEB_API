using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ComputersContext _computersContext;
        public CategoryRepository(ComputersContext computersContext)
        {
            _computersContext = computersContext;
        }

        public async Task<List<Category>> Get()
        {
            return await _computersContext.Categories.ToListAsync();
        }
    }
}
