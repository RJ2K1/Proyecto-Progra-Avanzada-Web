using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class DALGenericoImpl<TEntity> : IDALGenerico<TEntity> where TEntity : class
    {
        protected readonly ProyectoWebContext _Context;

        public DALGenericoImpl(ProyectoWebContext context)
        {
            _Context = context;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _Context.Set<TEntity>().AddAsync(entity);
            await _Context.SaveChangesAsync(); 
            return true;
        }


        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _Context.Set<TEntity>().ToListAsync();
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            _Context.Set<TEntity>().Remove(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _Context.Set<TEntity>().Attach(entity);
            _Context.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
