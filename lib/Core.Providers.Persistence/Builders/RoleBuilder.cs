namespace ExpenseTracker.Core.Providers.Persistence.Builders;

public class RoleBuilder : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));

        builder.HasIndex(x => x.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique(false);
    }
}

