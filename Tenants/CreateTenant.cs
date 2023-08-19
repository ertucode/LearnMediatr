using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LearnMediatr.Tenants;

public record CreateTenantCommand(string Name) : IRequest<Result<Tenant, string>>;

public class CreateTenantHandler : IRequestHandler<CreateTenantCommand, Result<Tenant, string>>
{
    private TenantContext _context;

    public CreateTenantHandler(TenantContext context)
    {
        _context = context;
    }

    public async Task<Result<Tenant, string>> Handle(
        CreateTenantCommand request,
        CancellationToken cancellationToken
    )
    {
        var existing = await _context.Tenants
            .Where(t => t.Name == request.Name)
            .FirstOrDefaultAsync();

        /* if (existing != null)
        {
            return new("Tenant name already exists");
        } */

        var tenant = new Tenant() { Name = request.Name };

        await _context.AddAsync(tenant);
        try
        {
            var res = await _context.SaveChangesAsync();
            return res == 0 ? new("Could not create tenant for some reason") : new(tenant);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
        {
            if (ex.InnerException is SqliteException exc)
            {
                if (exc.SqliteErrorCode == 19)
                {
                    var str = exc.Message.Split(": ")[2];
                    return new("UniqueConstraintFailed: " + str.Substring(0, str.Length - 2));
                }
            }
            return new("Could not create tenant for some reason");
        }
    }
}
