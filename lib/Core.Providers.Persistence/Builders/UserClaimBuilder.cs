namespace ExpenseTracker.Core.Providers.Persistence.Builders;

public class UserClaimBuilder : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable(nameof(UserClaim));

        builder.HasOne(e => e.User)
            .WithMany(c => c.UserClaims)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
