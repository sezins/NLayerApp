using Nlayer.Core.DTOs;
using Nlayer.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Service
{
    public interface ICategoryService:IService<Category>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetSingleCategoryByIdwithProductAsync(int categoryId);
    }
}
