using System;
using System.Reflection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Example.Business.Concreate;
using Example.Dal.Context;
using Example.Entities.Entities;
using Example.Common.Attributes;
using Example.Common.Helpers;
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

        // 1. Seed OperationClaims from Attributes
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

        // 2. Seed Admin User
        var adminEmail = "admin@example.com";
        var adminUser = await context.Users.FirstOrDefaultAsync(u => u.EMail == adminEmail, cancellationToken);

        if (adminUser == null)
        {
            HashHelpers.CreatePasswordHash("19Mayis1919!", out var passwordHash, out var passwordSalt);
            adminUser = new User
            {
                EMail = adminEmail,
                UserName = "admin",
                FirstName = "Admin",
                LastName = "User",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                ActiveFlg = true
            };
            await context.Users.AddAsync(adminUser, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        // 3. Assign Claims to Admin User
        var adminClaims = await context.OperationClaims
            .ToListAsync(cancellationToken);

        var existingUserClaims = await context.UserOperationClaims
            .Where(uoc => uoc.UserId == adminUser.Id)
            .Select(uoc => uoc.OperationClaimId)
            .ToListAsync(cancellationToken);

        var newUserClaims = adminClaims
            .Where(ac => !existingUserClaims.Contains(ac.Id))
            .Select(ac => new UserOperationClaim
            {
                UserId = adminUser.Id,
                OperationClaimId = ac.Id
            })
            .ToList();

        if (newUserClaims.Any())
        {
            await context.UserOperationClaims.AddRangeAsync(newUserClaims, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
