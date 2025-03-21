
using DepartmentAPI.Repo;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentAPI.Context;
using StudentAPI.DTOs;
using StudentAPI.Middleware;
using StudentAPI.Models;
using StudentAPI.Repo;
using System.Text;

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
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {
                op.Password.RequireLowercase = false;
                op.Password.RequiredLength = 4;
                op.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<StudentContext>();

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidAudience = "http://localhost:5127",
                    ValidateIssuer = false,
                    ValidIssuer = "http://localhost:5128/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e1045a50607bcbaadf16e640fba1a9bea8ab47d8706e30e4f8fa6529385d48b8"))
                };
            });

            builder.Services.AddDbContext<StudentContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("policy1");
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
