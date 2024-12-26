using PizzaApp.DomainModels.Entites;

namespace PizzaApp.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrderWithDetails();
    }
}
