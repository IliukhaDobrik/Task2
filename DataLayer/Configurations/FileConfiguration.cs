using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = DataLayer.Entities.File;

namespace DataLayer.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.HasKey(f => f.FileId);

        builder.HasIndex(f => f.FileName).IsUnique(true);
    }
}