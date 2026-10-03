using EmployeeManagement.Api.Middleware;
using EmployeeManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.

            builder.Services.AddControllers();
            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
