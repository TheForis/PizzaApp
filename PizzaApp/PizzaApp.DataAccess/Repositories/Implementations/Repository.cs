using Microsoft.EntityFrameworkCore;
using PizzaApp.DataAccess.DbContext;
using PizzaApp.DataAccess.Repositories.Interfaces;

namespace PizzaApp.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PizzaAppDbContext _context;
        public Repository(PizzaAppDbContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteById(int id)
        {
            try
            {
                var itemToDelete = await GetByIdInt(id);
                await Delete(itemToDelete);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                List<T> getAll = await _context.Set<T>().ToListAsync<T>();
                return  getAll;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetById(string id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetByIdInt(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
