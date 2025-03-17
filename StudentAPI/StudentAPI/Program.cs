
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Context;
using StudentAPI.DTOs;
using StudentAPI.Middleware;

namespace StudentAPI
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
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StudentContext>(options =>
            options.UseSqlServer("Server=MONICA\\SQLEXPRESS;Database=StudentDB;Trusted_Connection=True;Encrypt=False;"));
            builder.Services.AddCors(
                op => {
                    op.AddPolicy("policy1", policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(StudentMappingConfig).Assembly);
            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseAuthorization();

            app.UseCors("policy1");
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
