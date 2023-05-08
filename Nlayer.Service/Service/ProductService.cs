using AutoMapper;
using Nlayer.Core.DTOs;
using Nlayer.Core.Entity;
using Nlayer.Core.NewFolder;
using Nlayer.Core.Repositories;
using Nlayer.Core.Service;
using Nlayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Service
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitofWork unitofWork, IMapper mapper, IProductRepository productRepositor) : base(repository, unitofWork)
        {
            _mapper = mapper;
            _productRepository = productRepositor;
            
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductswithCategory();

            var productsDto=_mapper.Map<List<ProductWithCategoryDto>>(products);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }
    }
}
