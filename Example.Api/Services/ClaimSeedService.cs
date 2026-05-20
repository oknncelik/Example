using System;
using System.Reflection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Example.Business.Concreate;
using Example.Dal.Context;
using Example.Entities.Entities;
using Example.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Example.Api.Services;

public class ClaimSeedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public ClaimSeedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ExampleContext>();

        var businessAssembly = typeof(ProductManager).Assembly;
        var claimsFromAttributes = businessAssembly.GetTypes()
            .SelectMany(t => t.GetMethods())
            .SelectMany(m => m.GetCustomAttributes<AuthAttribute>(true))
            .Where(a => a.Roles != null)
            .SelectMany(a => a.Roles)
            .Distinct()
            .ToList();

        if (claimsFromAttributes.Any())
        {
            var existingClaims = await context.OperationClaims.Select(c => c.Name).ToListAsync(cancellationToken);
            var newClaims = claimsFromAttributes.Where(c => !existingClaims.Contains(c))
                .Select(c => new OperationClaim { Name = c })
                .ToList();

            if (newClaims.Any())
            {
                await context.OperationClaims.AddRangeAsync(newClaims, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
