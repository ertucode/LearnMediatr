using Microsoft.EntityFrameworkCore;

namespace LearnMediatr.Tenants;

[Index(nameof(Name), IsUnique = true)]
public class Tenant
{
    public long Id { get; set; }
    public required string Name { get; set; }
}
