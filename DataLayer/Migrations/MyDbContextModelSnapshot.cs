﻿// <auto-generated />
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataLayer.Entities.BankAccount", b =>
                {
                    b.Property<int>("BankAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BankAccountId"));

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("FileId")
                        .HasColumnType("int");

                    b.Property<decimal>("IncomingBalanceAsset")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IncomingBalanceLiability")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OutgoingBalanceAsset")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OutgoingBalanceLiability")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BankAccountId");

                    b.HasIndex("FileId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("DataLayer.Entities.File", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FileId");

                    b.HasIndex("FileName")
                        .IsUnique();

                    b.ToTable("Files");
                });

            modelBuilder.Entity("DataLayer.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("BankAccountId")
                        .HasColumnType("int");

                    b.Property<int>("FileId")
                        .HasColumnType("int");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionId");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("FileId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("DataLayer.Entities.BankAccount", b =>
                {
                    b.HasOne("DataLayer.Entities.File", "File")
                        .WithMany("BankAccounts")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("DataLayer.Entities.Transaction", b =>
                {
                    b.HasOne("DataLayer.Entities.BankAccount", "BankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.File", "File")
                        .WithMany("Transaction")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BankAccount");

                    b.Navigation("File");
                });

            modelBuilder.Entity("DataLayer.Entities.BankAccount", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("DataLayer.Entities.File", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
