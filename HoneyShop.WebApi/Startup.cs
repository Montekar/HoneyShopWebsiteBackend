using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoneyShop.Core.IServices;
using HoneyShop.DataAccess;
using HoneyShop.DataAccess.Repositories;
using HoneyShop.Domain;
using HoneyShop.Domain.IRepository;
using HoneyShop.Domain.Service;
using HoneyShop.Security;
using HoneyShop.Security.IRepositories;
using HoneyShop.Security.IServices;
using HoneyShop.Security.Models;
using HoneyShop.Security.Repositories;
using HoneyShop.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HoneyShopWebsiteBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
 
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "HoneyShop.WebApi", Version = "v1"});
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddAuthentication(authenticationOptions =>
            {
                authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidateLifetime = true
                };
            });
            var value = Configuration["JwtConfig:secret"];

            services.AddControllers();



            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

            services.AddDbContext<HoneyDbContext>(
                opt =>
                {
                    opt.UseLoggerFactory(loggerFactory)
                        .UseSqlite("Data Source=honeyShop.db");
                }, ServiceLifetime.Transient
            );

            services.AddDbContext<AuthDbContext>(
                opt =>
                {
                    opt.UseLoggerFactory(loggerFactory)
                        .UseSqlite("Data Source=honeyAuth.db");
                }, ServiceLifetime.Transient
            );

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerDetailsService, CustomerDetailsService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerDetailsRepository, CustomerDetailsRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IHoneyDbSeeder, HoneyDbSeeder>();


            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IAuthDbSeeder, AuthDbSeeder>();
            //services.AddScoped<IUserAuthenticator,UserAuthenticator>();
            // services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));
            services.AddCors(options =>
            {
                options.AddPolicy("Dev-cors", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHoneyDbSeeder honeyDbSeeder,IAuthDbSeeder authDbSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HoneyShop.WebApi v1"));
                app.UseCors("Dev-cors");
                honeyDbSeeder.SeedDevelopment();
                authDbSeeder.SeedDevelopment();
            }
            else
            {
                honeyDbSeeder.SeedProduction();
                authDbSeeder.SeedProduction();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthentication();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}