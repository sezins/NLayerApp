using Microsoft.EntityFrameworkCore;
using Nlayer.Core.Entity;
using Nlayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context)
        {
            
        }
        public async Task<List<Product>> GetProductswithCategory()
        {
            return await _appDbContext.Products.Include(p => p.Category).ToListAsync();
        }
    }
}
