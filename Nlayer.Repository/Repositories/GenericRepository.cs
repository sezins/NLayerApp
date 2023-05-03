using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Nlayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _appDbContext = context;
            _dbSet = _appDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            //Tracking özelliğini kapatıyoruz çünkü GetAll daha performanslı çalışması için. Read operasyonu olacağı için server tabanlı bir kayıt işlemi olmadığı için tracking özelliğini kapatıyoruz.Update-Insert-Delete yapmayacağız sadece datayı çekiyoruz.
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //Primary key bekler.
            return await _dbSet.FindAsync(id);
        }

        //Track edilen entity'nin State'ini delete eder. Yorum satırında yazılan ile Remove aynı işlemi yapar aslında. O sebeple Asenkron olmasına performans açısından gerek yoktur. 
        public void Remove(T entity)
        {
            //_context.Entry(entity).State = EntityState.Deleted;
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
