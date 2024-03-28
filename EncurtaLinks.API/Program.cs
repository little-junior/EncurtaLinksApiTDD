using EncurtaLinks.Core;
using EncurtaLinks.Core.Models;
using EncurtaLinks.API.Services;
using EncurtaLinks.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using EncurtaLinks.API.ErrorHandler;

namespace EncurtaLinks.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.AddCors();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EncurtaLinks API",
                    Description = "ASP.NET API de um encurtador de links feito em .NET 8 "
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddDbContext<EncurtaLinksContext>(context =>
            {
                context.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
            });

            builder.AddServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureExceptionHandler();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
