#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Example.Api.Models;
using Example.Common.DependencyResolvers;
using Example.Common.Extensions;
using Example.Common.Helpers;
using Example.Common.Ioc.Abstract;
using Example.Common.Security.Jwt.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

#endregion

namespace Example.Api
{
    public class Startup
    {
        private SwaggerInfo _swaggerInfo;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            _swaggerInfo = Configuration.GetSection(nameof(SwaggerInfo)).Get<SwaggerInfo>();
            var tokenOptions = Configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
            services.AddSingleton(tokenOptions);

            #region Swagger Settings

            services.AddSwaggerGen(options =>
            {
                options.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;

                options.AddSecurityDefinition(_swaggerInfo.Authorization.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = _swaggerInfo.Authorization.Description,
                    Name = _swaggerInfo.Authorization.Name,
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
                options.SwaggerDoc(_swaggerInfo.Version, new OpenApiInfo
                {
                    Title = _swaggerInfo.Title,
                    Version = _swaggerInfo.Version,
                    Description = _swaggerInfo.Description,
                    Contact = new OpenApiContact
                    {
                        Name = _swaggerInfo.Contact.Name,
                        Email = _swaggerInfo.Contact.Email
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

            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
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
                options.SwaggerEndpoint($"swagger/{_swaggerInfo.Version}/swagger.json", _swaggerInfo.Version);
            });

            #endregion

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}