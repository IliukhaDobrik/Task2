using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.HasKey(b => b.BankAccountId);

        builder.HasOne(ba => ba.File)
            .WithMany(f => f.BankAccounts)
            .HasForeignKey(ba => ba.FileId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}