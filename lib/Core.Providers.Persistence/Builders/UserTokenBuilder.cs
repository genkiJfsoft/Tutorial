namespace ExpenseTracker.Core.Providers.Persistence.Builders;

public class UserTokenBuilder : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(nameof(UserToken));

        builder.HasOne(e => e.User)
            .WithMany(c => c.Tokens)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
