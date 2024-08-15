using Microsoft.EntityFrameworkCore;

namespace Granite.Infrastructure.Data;

public class GraniteDatabaseContext : DbContext
{
    public GraniteDatabaseContext(DbContextOptions<GraniteDatabaseContext> options) : base(options)
    {
    }

    public GraniteDatabaseContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.RemovePluralizingTableNameConvention();
    }
}