
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using server.Models.DB;
using server.Policies;
using server.Services;
using server.Repositories;
using System;

namespace OnlineBookStore
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost4200",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            builder.Services.AddControllers();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();  
            builder.Logging.AddDebug();    

            var Audience = builder.Configuration.GetValue<string>("Audience");
            var Issuer = builder.Configuration.GetValue<string>("Issuer");
            var Security = builder.Configuration.GetValue<string>("Secret");

            var keybytes = System.Text.Encoding.UTF8.GetBytes(Security!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(c =>
            {
                c.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keybytes)
                };
            });

            builder.Services.AddAuthorization(c =>
            {
                c.AddPolicy(SecurityPolicy.Admin, SecurityPolicy.AdminPolicy());
                c.AddPolicy(SecurityPolicy.Customer, SecurityPolicy.CustomerPolicy());
            });

            builder.Services.AddDbContext<BookStoreDbContext>(config =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DevConnectionString")!;
                config.UseSqlServer(connectionString);
            });
            // Register Services
            builder.Services.AddTransient(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddTransient(typeof(IAuthRepo), typeof(AuthRepo));
            builder.Services.AddTransient(typeof(IPasswordHasher<User>), typeof(PasswordHasher<User>));
            builder.Services.AddTransient(typeof(ITokenGenerator), typeof(JwtTokenGenerator));
            builder.Services.AddTransient(typeof(IBookRepo),typeof(BookRepo));
            builder.Services.AddTransient(typeof(IBookService),typeof(BookService));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Logging.ClearProviders();
            builder.Logging.AddEventSourceLogger();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseCors("AllowLocalhost4200");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            Console.WriteLine("Server is up: http://localhost:5187");
            Console.WriteLine("Swagger is up: http://localhost:5187/swagger/index.html");
            app.Run();

        }
    }
}