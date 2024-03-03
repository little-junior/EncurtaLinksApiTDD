using EncurtaLinks.API.Filters;
using EncurtaLinks.Core.Models;
using EncurtaLinks.Core.Services;
using EncurtaLinks.Data.Contexts;
using EncurtaLinks.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace EncurtaLinks.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EncurtaLinks API",
                    Description = "ASP.NET Web API de um encurtador de links feito em .NET 8 "
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddDbContext<EncurtaLinksContext>(context =>
            {
                context.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
            });

            builder.Services.AddScoped(typeof(IRepository<LinkEncurtado>), typeof(EncurtaLinksRepository));
            builder.Services.AddScoped(typeof(IEncurtaLinksService), typeof(EncurtaLinksService));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
