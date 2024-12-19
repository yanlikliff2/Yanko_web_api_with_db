using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Yanko_web3_v2.Authorization;
using Yanko_web3_v2.Helpers;
using Yanko_web3_v2.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<PractDbContext>(
                options => options.UseSqlServer(builder.Configuration["ConnectionString"]));
            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(x => {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/as
            // pnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMapster();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope()) 
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PractDbContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder.WithOrigins(new[] { "https://localhost:7098", "https://yanko-web-api-with-db.onrender.com" })
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()
            );


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMidleware>();

            app.UseMiddleware<JwtMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
