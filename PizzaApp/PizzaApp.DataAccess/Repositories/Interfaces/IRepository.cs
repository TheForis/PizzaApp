
namespace PizzaApp.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task DeleteById(int id);
        Task Update(T entity);
        Task<T> GetByIdInt (int id);
        Task<T> GetById (string id);
        Task<List<T>> GetAll();
        Task SaveChanges();


    }
}
