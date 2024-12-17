namespace ExpenseTracker.Core.Providers.Persistence.Builders;

public class RoleClaimBuilder : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable(nameof(RoleClaim));

        builder.HasOne(e => e.Role)
            .WithMany(p => p.RoleClaims)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
