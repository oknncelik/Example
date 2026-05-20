#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Example.Api.Models;
using Example.Business.Mappings.AutoMapper;
using Example.Common.DependencyResolvers;
using Example.Common.Extensions;
using Example.Common.Helpers;
using Example.Common.Ioc.Abstract;
using Example.Common.Security.Jwt.Models;
using Example.Core.DependencyResolvers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Autofac Configuration
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacModule());
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<Example.Dal.Context.ExampleContext>();
builder.Services.AddHostedService<Example.Api.Services.ClaimSeedService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        policyBuilder => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var swaggerInfo = builder.Configuration.GetSection(nameof(SwaggerInfo)).Get<SwaggerInfo>();
var tokenOptions = builder.Configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
builder.Services.AddSingleton(tokenOptions);

#region Swagger Settings

builder.Services.AddSwaggerGen(options =>
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowOrigin");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

#region Swagger

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = string.Empty; // set start page swagger.
    options.SwaggerEndpoint($"swagger/{swaggerInfo.Version}/swagger.json", swaggerInfo.Version);
});

#endregion

app.MapControllers();

app.Run();
