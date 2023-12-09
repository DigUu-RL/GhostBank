﻿// <auto-generated />
using System;
using GhostBank.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GhostBank.Infrastructure.Data.Migrations
{
    [DbContext(typeof(GhostBankContext))]
    partial class GhostBankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Bank.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Agency")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<decimal>("Balance")
                        .HasColumnType("DECIMAL");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("Excluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Bank.Pix", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("Excluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("DATETIME");

                    b.Property<decimal>("Limit")
                        .HasColumnType("DECIMAL");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Pix", (string)null);
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Cards.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("Excluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Card", (string)null);

                    b.HasDiscriminator<string>("Type").HasValue("Default");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Cards.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("DECIMAL");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("Excluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("BIT");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("DATETIME");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("DECIMAL");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Identity.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DATETIME");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("Excluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CNPJ")
                        .HasMaxLength(14)
                        .HasColumnType("CHAR");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("CHAR");

                    b.Property<string>("Cellphone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("Excluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Identity.UserClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("Excluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Cards.CreditCard", b =>
                {
                    b.HasBaseType("GhostBank.Infrastructure.Data.Entities.Cards.Card");

                    b.Property<decimal>("Limit")
                        .HasColumnType("DECIMAL");

                    b.HasDiscriminator().HasValue("Credit");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Cards.DebitCard", b =>
                {
                    b.HasBaseType("GhostBank.Infrastructure.Data.Entities.Cards.Card");

                    b.HasDiscriminator().HasValue("Debit");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Cards.VirtualCard", b =>
                {
                    b.HasBaseType("GhostBank.Infrastructure.Data.Entities.Cards.Card");

                    b.Property<bool>("IsCredit")
                        .HasColumnType("BIT");

                    b.Property<decimal?>("Limit")
                        .HasColumnType("DECIMAL");

                    b.ToTable("Card", t =>
                        {
                            t.Property("Limit")
                                .HasColumnName("VirtualCard_Limit");
                        });

                    b.HasDiscriminator().HasValue("Virtual");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Bank.Account", b =>
                {
                    b.HasOne("GhostBank.Infrastructure.Data.Entities.Identity.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Bank.Pix", b =>
                {
                    b.HasOne("GhostBank.Infrastructure.Data.Entities.Bank.Account", "Account")
                        .WithMany("Pix")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Cards.Card", b =>
                {
                    b.HasOne("GhostBank.Infrastructure.Data.Entities.Bank.Account", "Account")
                        .WithMany("Cards")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Cards.Invoice", b =>
                {
                    b.HasOne("GhostBank.Infrastructure.Data.Entities.Cards.CreditCard", "Card")
                        .WithMany("Invoices")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Identity.User", b =>
                {
                    b.HasOne("GhostBank.Infrastructure.Data.Entities.Identity.Address", "Address")
                        .WithOne("User")
                        .HasForeignKey("GhostBank.Infrastructure.Data.Entities.Identity.User", "AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Identity.UserClaim", b =>
                {
                    b.HasOne("GhostBank.Infrastructure.Data.Entities.Identity.User", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Bank.Account", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Pix");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Identity.Address", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Identity.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Claims");
                });

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Cards.CreditCard", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
