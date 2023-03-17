namespace SuperSimpleArchitecture.Fitnet.Passes.Persistence;

using Microsoft.EntityFrameworkCore;
using Domain;

internal sealed class PassesPersistence : DbContext
{
    private const string Schema = "Passes";
    
    public PassesPersistence(DbContextOptions<PassesPersistence> options)
        : base(options)
    {
    }

    public DbSet<Pass> Passes => Set<Pass>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new PassEntityConfiguration());
    }
}