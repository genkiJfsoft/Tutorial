namespace ExpenseTracker.Core.Providers.Persistence.Builders;

internal class UserBuilder : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasMany(e => e.UserClaims)
            .WithOne(c => c.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.HasMany(e => e.Logins)
            .WithOne(c => c.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.HasMany(e => e.Tokens)
            .WithOne(c => c.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.HasMany(e => e.UserRoles)
            .WithOne(c => c.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}
