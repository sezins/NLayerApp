using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nlayer.Core.DTOs;
using Nlayer.Core.Entity;
using Nlayer.Core.Service;

namespace Nlayer.Apı.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;
        public ProductsController(IMapper mapper, IService<Product> service)
        {
            _service = service;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> All()
        {
            var products=await _service.GetAllAsync();
            var productsDtos=_mapper.Map<List<ProductDto>>(products.ToList());

            return Ok(CustomResponseDto<List<ProductDto>>.Success(200,productsDtos));
        }


    }
}
