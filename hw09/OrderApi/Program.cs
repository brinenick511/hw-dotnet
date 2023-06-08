using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFramework;
using MySql.EntityFrameworkCore;

using OrderApi.Models;

namespace OrderApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            String? connnectionString = builder.Configuration.GetConnectionString("orderDB");
            builder.Services.AddDbContext<OrderContext>(opt => opt.UseMySQL(connnectionString));
            builder.Services.AddScoped<OrderService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}