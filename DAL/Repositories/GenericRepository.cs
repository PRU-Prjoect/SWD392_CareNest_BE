using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private DbSet<T> _dbset;
        public GenericRepository(ApplicationDbContext context)
        {
            _dbset = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            entity.created_at = DateTime.UtcNow;
            entity.updated_at = DateTime.UtcNow;
            await _dbset.AddAsync(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            entity.updated_at = DateTime.UtcNow;
            _dbset.Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            entity.updated_at = DateTime.UtcNow;
            _dbset.Update(entity);
        }
    }
}
