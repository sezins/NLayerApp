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
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> repository, IUnitofWork unitofWork, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitofWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;            
        }
        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetSingleCategoryByIdwithProductAsync(int categoryId)
        {
            
        }
    }
}
