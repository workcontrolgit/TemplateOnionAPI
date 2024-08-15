using $ext_projectname$.Application.Interfaces;
using $safeprojectname$.Contexts;
using EFCore.BulkExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace $safeprojectname$.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task BulkInsertAsync(IEnumerable<T> entities)
        {
            // Bulk Insert Extension https://entityframework-extensions.net/bulk-insert
            await _dbContext.BulkInsertAsync(entities);

            // if DB does not support bulk insert use the code below
            //foreach (T row in entities)
            //{
            //    await this.AddAsync(row);
            //}
        }

        public async Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPagedAdvancedReponseAsync(int pageNumber, int pageSize, string orderBy, string fields, ExpressionStarter<T> predicate)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select<T>("new(" + fields + ")")
                .OrderBy(orderBy)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllShapeAsync(string orderBy, string fields)
        {
            return await _dbContext
                .Set<T>()
                .Select<T>("new(" + fields + ")")
                .OrderBy(orderBy)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}