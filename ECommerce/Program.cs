using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Contracts.Seed;
using ECommerce.Domain.Contracts.UOW;
using ECommerce.Middleware;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Identity.Models;
using ECommerce.Persistence.Repos;
using ECommerce.Persistence.Seed;
using ECommerce.Persistence.UOW;
using ECommerce.Service.MappingProfiles;
using ECommerce.Service.Services;
using ECommerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace ECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            #region Database
            builder.Services.AddDbContext<StoreDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            builder.Services.AddDbContext<StoredIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoredIdentityDbContext>();

            #endregion

            #region Bisness Services
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServicesManger, ServicesManger>();
            builder.Services.AddScoped<IBasketReposatory, BasketReposatory>();
            #endregion


            builder.Services.AddAutoMapper(m => m.AddProfile(new ProjectProfile(builder.Configuration)));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(x => new ValidationError
                        {
                            Field = x.Key,
                            Errors = x.Value.Errors.Select(err => err.ErrorMessage)
                        });
                    var errorResponse = new ValidationErrorToReturn
                    {
                        ValidationErrors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            builder.Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration.GetSection("JWTOptions")["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration.GetSection("JWTOptions")["Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTOptions")["SecurityKey"])),
                };
            });

            var app = builder.Build();

            var Scope = app.Services.CreateScope();
            var ObjectSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            ObjectSeeding.DataSeedAsync();
            ObjectSeeding.IdentitySeedAsync();

            app.UseMiddleware<CutomExceptionMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
