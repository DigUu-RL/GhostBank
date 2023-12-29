﻿// <auto-generated />
using System;
using GhostBank.Infrastructure.Data.Contexts.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GhostBank.Infrastructure.Data.Migrations.GhostBankAudit
{
    [DbContext(typeof(GhostBankAuditContext))]
    partial class GhostBankAuditContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GhostBank.Infrastructure.Data.Entities.Audit.Identity.UserAudit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Actor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Column")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATETIME");

                    b.Property<string>("NewValue")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("OldValue")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("UserAudit", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
