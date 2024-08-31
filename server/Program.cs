
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

            // Add services to the container.
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            var Aud = builder.Configuration.GetValue<string>("Audience");
            var iss = builder.Configuration.GetValue<string>("Issuer");
            var sec = builder.Configuration.GetValue<string>("Secret");

            var keybytes = System.Text.Encoding.UTF8.GetBytes(sec);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(c =>
            {
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = iss,
                    ValidAudience = Aud,
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
            builder.Services.AddTransient(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddTransient(typeof(IAuthRepo), typeof(AuthRepo));
            builder.Services.AddTransient(typeof(IPasswordHasher<User>), typeof(PasswordHasher<User>));
            builder.Services.AddTransient(typeof(ITokenGenerator), typeof(JwtTokenGenerator));
            builder.Services.AddTransient(typeof(IBookRepo), typeof(BookRepo));
            builder.Services.AddTransient(typeof(IBookService), typeof(BookService));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Logging.ClearProviders();
            //builder.Logging.AddLog4Net();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowLocalhost4200");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}