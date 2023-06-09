﻿using Microsoft.EntityFrameworkCore;
using Nlayer.Core.NewFolder;
using Nlayer.Core.Repositories;
using Nlayer.Core.Service;
using Nlayer.Repository.UnitOfWork;
using Nlayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Service
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitofWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitofWork unitofWork)
        {
            _repository = repository;
            _unitOfWork = unitofWork;
        }
        public async Task<T> AddAsync(T entity)
        {
            //Savechange sonrası entity'i geri döndürerek Sql tarafında otomatik oluşturulan Id'yi çekmiş oluyoruz. Bu sayede bu Id'yi "2" nolu Id başarıyla eklendi vs. gibi yazdırabiliriz.
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct= await _repository.GetByIdAsync(id);

            if (hasProduct==null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            return hasProduct;
        }
        //SaveChange asenkron olduğu için yani CommitAsync Remove ve update'i asenkron olarak tanımladık.
        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }

    }
}
