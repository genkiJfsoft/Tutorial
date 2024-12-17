namespace ExpenseTracker.Core.Providers.Persistence.Builders;

internal class ExpenseBuilder : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable(nameof(Expense));
        builder.Property(t => t.Amount).HasColumnType("DECIMAL(18,2)");

        builder.HasKey(t => t.Id);
    }
}
