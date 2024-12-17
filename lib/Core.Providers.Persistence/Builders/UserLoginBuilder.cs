namespace ExpenseTracker.Core.Providers.Persistence.Builders;

public class UserLoginBuilder : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable(nameof(UserLogin));

        builder.HasOne(e => e.User)
            .WithMany(c => c.Logins)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
