﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Infrastructure.Data;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Specifications;
using Shaghaf.Core.Entities.HomeEntities;

namespace Shaghaf.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {

            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public void Add(T entity)
       => _context.Set<T>().Add(entity);

        public void Update(T entity)
        { /*=>_context.Set<T>().Update(entity);*/
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
                => _context.Set<T>().Remove(entity);
    }
}
