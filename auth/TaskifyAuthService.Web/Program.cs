using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taskify.DAL;
using Taskify.Utils.Extensions;
using Taskify.Utils.Logging;
using Taskify.Utils.Middlewares;
using TaskifyAuthService.Web.Services;
using TaskifyAuthService.Web.Utils;

namespace TaskifyAuthService.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("TaskifyDb");

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
            builder.Services
                .AddDbContext<TaskifyDbContext>(options => options.UseNpgsql(connectionString));
            builder.AddLogger();

            builder.Services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<TaskifyDbContext>();
            builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            var app = builder.Build();
            app.UseExceptionHandlingMiddleware();

            if (app.Environment.IsDevelopment())
            {
                SeedDevData(app);
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            app.MapControllers();
            app.UseRouting();

            app.Run();
        }

        private static void SeedDevData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                AdminUserInitializer.SeedAdmin(userManager);
            }
        }
    }
}