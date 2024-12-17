using To_Do_List.BLL.Repositories.Interfaces;
using To_Do_List.DAL.Models;
using To_Do_List.DAL.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.BLL.Repositories.Repositories
{
    public class GenericReposiory<T> : IGenericRepository<T> where T : class
    {
        private protected readonly ToDoListContext _dbContext;

        public GenericReposiory(ToDoListContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return (IQueryable<T>)_dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
