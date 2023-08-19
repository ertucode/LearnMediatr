using LearnMediatr.Tenants;
using Microsoft.EntityFrameworkCore;

namespace LearnMediatr;

public class TenantContext : DbContext
{
    public DbSet<Tenant> Tenants { get; set; } = null!;

    public string DbPath { get; private init; }

    public TenantContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "tenant.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"DataSource={DbPath}");
    }
}
