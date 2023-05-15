using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nlayer.Apı.Filter;
using Nlayer.Core.DTOs;
using Nlayer.Core.Entity;
using Nlayer.Core.Service;

namespace Nlayer.Apı.Controllers
{
    [ValidateFilterAttribute]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;
        private readonly IProductService productService;
        public ProductsController(IMapper mapper, IService< Product> service, IProductService productService)
        {
            _service = service;
            _mapper = mapper;
            this.productService = productService;
        }

        //api/products/GetProductWithCategory
        //[HttpGet("[action]")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory() 
        {
            //ozellesmıs bır data oldugu ıcın repo ve servıcede yazıyoruz
           return CreateActionResult(await productService.GetProductsWithCategory());
        }
        //api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products=await _service.GetAllAsync();
            var productsDtos=_mapper.Map<List<ProductDto>>(products.ToList());

            return Ok(CustomResponseDto<List<ProductDto>>.Success(200,productsDtos));
        }
        //api/procucts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _service.GetByIdAsync(id);

            //if (products==null)
            //{
            //    return CreateActionResult(CustomResponseDto<List<ProductDto>>.Fail(400, "Bu id'ye sahip ürün bulunmamaktadır."));
            //}

            var productsDtos = _mapper.Map<List<ProductDto>>(products);
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDtos = _mapper.Map<List<ProductDto>>(product);
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(201, productsDtos));
        }


        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            //Donustıpı olmadıgı ıcın nocontent donduk
            return CreateActionResult(CustomResponseDto<List<NoContentDto>>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
