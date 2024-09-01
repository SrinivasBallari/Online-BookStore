
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using server.Models.DB;
using server.Policies;
using server.Services;
using server.Repositories;
using System;
using System.Text.Json.Serialization;


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
           

            builder.Services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
               });

            var audience = builder.Configuration.GetValue<string>("Audience");
            var issuer = builder.Configuration.GetValue<string>("Issuer");
            var secret = builder.Configuration.GetValue<string>("Secret");

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(secret);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(c =>
                {
                    c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                    };
                    c.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            Console.WriteLine($"Exception StackTrace: {context.Exception.StackTrace}");
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = new { message = "Authentication failed." };
                            return context.Response.WriteAsJsonAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = new { message = "Access denied." };
                            return context.Response.WriteAsJsonAsync(result);
                        }
                    };
                });


            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy(SecurityPolicy.Admin, SecurityPolicy.AdminPolicy());
                config.AddPolicy(SecurityPolicy.Customer, SecurityPolicy.CustomerPolicy());
            });

            builder.Services.AddDbContext<BookStoreDbContext>(config =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DevConnectionString")!;
                config.UseSqlServer(connectionString);
            });
            builder.Services.AddTransient(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddTransient(typeof(IAuthRepo), typeof(AuthRepo));
            builder.Services.AddTransient(typeof(IOrderRepo), typeof(OrderRepo));
            builder.Services.AddTransient(typeof(IPasswordHasher<User>),typeof(PasswordHasher<User>));
            builder.Services.AddTransient(typeof(ITokenGenerator),typeof(JwtTokenGenerator));
            builder.Services.AddTransient(typeof(IOrderService), typeof(OrderService));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Logging.ClearProviders();
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowLocalhost4200");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}