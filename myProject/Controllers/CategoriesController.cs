using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace myProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryServieces _categoryServieces;
        private IMapper _mapper;


        public CategoriesController(ICategoryServieces categoryServieces,IMapper mapper)
        {
            _categoryServieces = categoryServieces;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> Get()
        {
            List<CategoryDto> categories = _mapper.Map<List<Category>, List<CategoryDto>>(await _categoryServieces.Get());
            return Ok(categories);
        }

    }
}
