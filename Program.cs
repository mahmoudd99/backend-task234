using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Task2.Data;
using Task2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        // Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddDbContext<AppDbContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],

                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });
        builder.Services.AddAuthorization();



        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular",
                policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        var app = builder.Build();

        // Exception middleware 
        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseCors("AllowAngular");
        app.UseAuthentication();
        app.UseAuthorization();
      

        app.MapControllers();

        app.Run();
    }
}