using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Nlayer.Core.Service;

namespace Nlayer.Apı.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //api/categories/etSingleCategoryByIdwithProducts/2
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdwithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdwithProductAsync(categoryId));
        }
    }
}
