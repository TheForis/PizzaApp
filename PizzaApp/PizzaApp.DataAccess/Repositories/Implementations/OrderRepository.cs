using Microsoft.EntityFrameworkCore;
using PizzaApp.DataAccess.DbContext;
using PizzaApp.DataAccess.Repositories.Interfaces;
using PizzaApp.DomainModels.Entites;

namespace PizzaApp.DataAccess.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly PizzaAppDbContext _context;
        public OrderRepository(PizzaAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderWithDetails()
        {
            var result = await _context.Order
                .Include(x=> x.Pizzas)
                .Include(u=> u.User)
                .ToListAsync();
            return result;
        }
    }
}
