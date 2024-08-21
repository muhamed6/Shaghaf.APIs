using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shaghaf.Application.Mappings;
using Shaghaf.Application.Services;
using Shaghaf.Core;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Infrastructure;
using Shaghaf.Infrastructure.Data;
using Shaghaf.Infrastructure.Repositories;
using Shaghaf.Service;
using System.Text.Json.Serialization;
using Stripe;
using Shaghaf.API.Helpers;
using Shaghaf.Infrastructure.Sevices.Implementaion;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Shaghaf.Core.Entities.Identity;
using Shaghaf.Infrastructure.Identity;
using Shaghaf.Service.AuthService;
using Shaghaf.API.Extensions;

namespace Shaghaf.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.MapType<Newtonsoft.Json.Linq.JObject>(() => new OpenApiSchema { Type = "object" });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();


            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IRoomService), typeof(RoomService));
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IPhotoSessionService, PhotoSessionService>();
            builder.Services.AddScoped<ICakeService, CakeService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped< ICategoryService, CategoryService>();
            builder.Services.AddScoped<IBirthDayService, BirthDayService>();
            builder.Services.AddScoped<IDecorationService, DecorationService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add Stripe configuration settings
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

            // Retrieve Stripe settings and set the API key
            var stripeSettings = builder.Configuration.GetSection("Stripe").Get<StripeSettings>();
            if (stripeSettings == null)
            {
                throw new ArgumentNullException("StripeSettings are not configured correctly in appsettings.json.");
            }
            Console.WriteLine($"Stripe Secret Key: {stripeSettings.SecretKey}");
             StripeConfiguration.ApiKey = stripeSettings.SecretKey;
            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            builder.Services.AddAuthServices(builder.Configuration);


            var app = builder.Build();

            #region UpdateDataBaseWhenRun


            using var scope = app.Services.CreateScope(); // lma a5ls hi3ml dispose ll scope w y2fl kol l objects eli atlbt mn scope
            var services = scope.ServiceProvider;
            //var _dbContext = services.GetRequiredService<StoreContext>();
            var _identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();
            // ask CLR for Creating Object from DbContext Explicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // to log error in kestrel
            try
            {

                await _identityDbContext.Database.MigrateAsync(); 
                
                var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>(); 
                await ApplicationIdentityContextSeed.SeedUserAsync(_userManager, _roleManager); 
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration");
            }

            #endregion



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
