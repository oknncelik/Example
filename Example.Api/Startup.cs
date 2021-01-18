using Example.Common.Security.Jwt.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Example.Common.Helpers;
using Example.Api.Models;

namespace Example.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private SwaggerInfo swaggerInfo;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            swaggerInfo = Configuration.GetSection(nameof(SwaggerInfo)).Get<SwaggerInfo>();
            var tokenOptions = Configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
            services.AddSingleton(tokenOptions);

            #region Swagger Settings
            services.AddSwaggerGen(options =>
            {
                options.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;

                options.AddSecurityDefinition(swaggerInfo.Authorization.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = swaggerInfo.Authorization.Description,
                    Name = swaggerInfo.Authorization.Name,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                var security = new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            },
                            UnresolvedReference = true
                        },
                        new List<string>()
                    }
                };
                options.AddSecurityRequirement(security);
                options.SwaggerDoc(swaggerInfo.Version, new OpenApiInfo
                {
                    Title = swaggerInfo.Title,
                    Version = swaggerInfo.Version,
                    Description = swaggerInfo.Description,
                    Contact = new OpenApiContact
                    {
                        Name = swaggerInfo.Contact.Name,
                        Email = swaggerInfo.Contact.Email
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            #endregion

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = tokenOptions.Key.CreateSecurityKey()
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty; //set start page swagger.
                options.SwaggerEndpoint($"swagger/{swaggerInfo.Version}/swagger.json", swaggerInfo.Version);
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
