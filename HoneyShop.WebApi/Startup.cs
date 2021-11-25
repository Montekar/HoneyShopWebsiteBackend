using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoneyShop.Core.IServices;
using HoneyShop.DataAccess;
using HoneyShop.DataAccess.Repositories;
using HoneyShop.Domain;
using HoneyShop.Domain.IRepository;
using HoneyShop.Domain.Service;
using HoneyShop.Security.Helpers;
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
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
                
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "HoneyShop.WebApi", Version = "v1"});
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("dev-cors", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            
            services.AddDbContext<HoneyContext>(
                opt =>
                {
                    opt.UseLoggerFactory(loggerFactory)
                        .UseSqlite("Data Source=honeyShop.db");
                }, ServiceLifetime.Transient
            );
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerDetailsService, CustomerDetailsService>();
            services.AddScoped<IUserService,UserService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerDetailsRepository, CustomerDetailsRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            
            services.AddScoped<IUserAuthenticator,UserAuthenticator>();

            services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HoneyShop.WebApi v1"));
                app.UseCors("dev-cors");
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<HoneyContext>();
                    DbInitialize.InitData(context);
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}