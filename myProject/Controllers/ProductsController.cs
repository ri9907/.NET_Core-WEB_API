using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace myProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductServieces _productServieces;
        private IMapper _mapper;

        public ProductsController(IProductServieces productServieces, IMapper mapper)
        {
            _productServieces = productServieces;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get( int? minPrice, int? maxPrice, [FromQuery] List< int>? categoriesId, string? description)
        {
            List <ProductDTO> products =  _mapper.Map<List<Product>,List<ProductDTO>>(await _productServieces.Get(minPrice, maxPrice, categoriesId, description));
            return Ok(products);
        }
    }
}
