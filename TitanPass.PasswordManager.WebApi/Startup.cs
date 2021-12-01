using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.DB;
using TitanPass.PasswordManager.DB.Repositories;
using TitanPass.PasswordManager.Domain.IRepositories;
using TitanPass.PasswordManager.Domain.Services;
using TitanPass.PasswordManager.Security;
using TitanPass.PasswordManager.Security.IRepositories;
using TitanPass.PasswordManager.Security.IServices;
using TitanPass.PasswordManager.Security.Repositories;
using TitanPass.PasswordManager.Security.Services;

namespace TitanPass.PasswordManager.WebApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TitanPass.PasswordManager.WebApi", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });
            
            services.AddAuthentication(authenticationOptions =>
                {
                    authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = 
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])),
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],
                        ValidateLifetime = true
                    };
                });

            //Setting up dependency injection
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IPasswordManagerDbContextSeeder, PasswordManagerDbContextSeeder>();

            
            //Setting up dependency injection for security
            services.AddScoped<ILoginCustomerRepository, LoginCustomerRepository>();
            services.AddScoped<ILoginCustomerService, LoginCustomerService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ISecurityDbContextSeeder, SecurityDbContextSeeder>();
            
            
            var loggerFactory = LoggerFactory.Create(builder => {
                    builder.AddConsole();
                }
            );
            
            services.AddDbContext<PasswordManagerDbContext>(
                opt =>
                {
                    opt
                        .UseLoggerFactory(loggerFactory)
                        .UseSqlite("Data Source=main.db");
                }, ServiceLifetime.Transient);
            
            services.AddDbContext<SecurityDbContext>(
                options =>
                {
                    options
                        .UseLoggerFactory(loggerFactory)
                        .UseSqlite("Data Source=auth.db");
                });

            
            services.AddCors(options => options
                .AddPolicy("dev-policy", policy =>
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISecurityDbContextSeeder securityDbContextSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TitanPass.PasswordManager.WebApi v1"));
                
                app.UseCors("dev-policy");
                securityDbContextSeeder.SeedDevelopment();
                
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var ctx = services.GetService<PasswordManagerDbContext>();
                    ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();
            
            app.UseCors("dev-policy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}