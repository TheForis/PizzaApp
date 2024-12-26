using PizzaApp.Helpers.DIContainer;
using PizzaApp.Helpers.Extensions;
using PizzaApp.Mappers.MapperConfig;
namespace PizzaApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var appSettings = builder.Configuration.GetSection("AppSettings");
            // Add services to the container.

            builder.Configuration.AddEnvironmentVariables();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly)
                            .AddPostgreSqlDbContext(appSettings)
                            .AddAuthentication()
                            .AddJwt(appSettings)
                            .AddIdentity()
                            .AddCors()
                            .AddSwagger();



            DIHelper.InjectRepositories(builder.Services);
            DIHelper.InjectServices(builder.Services);




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            

            app.UseHttpsRedirection();
            app.UseCors(builder=>
                        builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
            );

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
