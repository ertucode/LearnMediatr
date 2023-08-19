using MediatR;

namespace LearnMediatr.Tenants;

public record CreateTenantCommand(string Display) : IRequest<Result<Tenant, string>>;

public class CreateTenantHandler : IRequestHandler<CreateTenantCommand, Result<Tenant, string>>
{
    public async Task<Result<Tenant, string>> Handle(
        CreateTenantCommand request,
        CancellationToken cancellationToken
    )
    {
        return new(";");
    }
}
