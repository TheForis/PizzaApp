using PizzaApp.DataAccess.DbContext;
using PizzaApp.DataAccess.Repositories.Interfaces;
using PizzaApp.DomainModels.Entites;

namespace PizzaApp.DataAccess.Repositories.Implementations
{
    public class PizzaRepository : Repository<Pizza>, IPizzaRepository
    {
        private readonly PizzaAppDbContext _context;
        public PizzaRepository(PizzaAppDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
