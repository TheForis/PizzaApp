using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.DataAccess.DbContext;
using PizzaApp.DataAccess.Repositories.Implementations;
using PizzaApp.DataAccess.Repositories.Interfaces;
using PizzaApp.Services.OrderServices.Implementations;
using PizzaApp.Services.OrderServices.Interfaces;
using PizzaApp.Services.PizzaServices.Implementations;
using PizzaApp.Services.PizzaServices.Interfaces;
using PizzaApp.Services.UserServices.Implementations;
using PizzaApp.Services.UserServices.Interfaces;

namespace PizzaApp.Helpers.DIContainer
{
    public static class DIHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PizzaAppDbContext>(x=> x.UseNpgsql(connectionString));
        }
        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IPizzaRepository, PizzaRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPizzaService, PizzaService>();
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}
