using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.TransactionId);

        builder.HasOne(q => q.BankAccount)
            .WithMany(ba => ba.Transactions)
            .HasForeignKey(t => t.BankAccountId);

        builder.HasOne(t => t.File)
            .WithMany(f => f.Transaction)
            .HasForeignKey(t => t.FileId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}