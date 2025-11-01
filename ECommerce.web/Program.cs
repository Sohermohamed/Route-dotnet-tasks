
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contracts.Seeds;
using ECommerce.Domain.Contracts.UOW;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.DataSeed;
using ECommerce.Persistence.UOW;
using ECommerce.Service.MappingProfile;
using ECommerce.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using System.Threading.Tasks;

namespace ECommerce.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(m=>m.AddProfile(new ProjectProfile()));

            var app = builder.Build();
            var Scope = app.Services.CreateScope();
            var ObjectSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
             await  ObjectSeeding.DataSeed();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
