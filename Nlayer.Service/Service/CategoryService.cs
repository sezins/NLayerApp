using AutoMapper;
using Nlayer.Core.DTOs;
using Nlayer.Core.Entity;
using Nlayer.Core.Repositories;
using Nlayer.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Service
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper)
        {
            
        }
        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetSingleCategoryByIdwithProductAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
