using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryServieces : ICategoryServieces
    {
        private ICategoryRepository _categoryRepository;

        public CategoryServieces(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> Get()
        {
            return await _categoryRepository.Get();
        }
    }
}
