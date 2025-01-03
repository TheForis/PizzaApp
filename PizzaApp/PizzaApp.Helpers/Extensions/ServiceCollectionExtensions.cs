﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PizzaApp.DataAccess.DbContext;
using PizzaApp.DomainModels.Entites;
using Swashbuckle.AspNetCore.Filters;
using System.Runtime.CompilerServices;
using System.Text;

namespace PizzaApp.Helpers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public class ConfigBuilder
        {
            public IServiceCollection Services { get; set; }
            public IConfiguration Configuration { get; set; }
            public IdentityBuilder IdentityBuilder { get; set; }
            public AuthenticationBuilder AuthenticationBuilder { get; set; }
            public ConfigBuilder(IServiceCollection services, IConfiguration configuration)
            {
                Services = services;
                Configuration = configuration;
            }
        }
        public static ConfigBuilder AddPostgreSqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").Value;
            services.AddDbContext<PizzaAppDbContext>(options => options.UseNpgsql(connectionString));
                return new(services,configuration);
         }
        public static ConfigBuilder AddSwagger(this ConfigBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the bearer scheme, eg." + "\\bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            return builder;
        }
        public static ConfigBuilder AddIdentity(this ConfigBuilder builder)
        {
            builder.Services.AddIdentityCore<User>(option =>
            {
                option.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<PizzaAppDbContext>()
                .AddDefaultTokenProviders();
            return builder;
        }
        public static ConfigBuilder AddCors(this ConfigBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CPRSPolicy", builder => builder.AllowAnyMethod()
                                                                  .AllowCredentials()
                                                                  .AllowAnyHeader()
                                                                  .SetIsOriginAllowed((hosts) => true));

            });
            return builder;
        }
        public static ConfigBuilder AddAuthentication (this ConfigBuilder builder)
        {
            builder.AuthenticationBuilder = builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            return builder;
        }
        public static ConfigBuilder AddJwt(this ConfigBuilder builder, IConfiguration configuration)
        {
            var token = configuration.GetSection("SecretTokenPhrase").Value;
            builder.AuthenticationBuilder.AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return builder;
        }
    }
}
