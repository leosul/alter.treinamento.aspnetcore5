﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using alter.treinamento.data.Context;

namespace alter.treinamento.data.Migrations
{
    [DbContext(typeof(AlterDbContext))]
    [Migration("20210913103813_001")]
    partial class _001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("alter.treinamento.business.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Description");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("alter.treinamento.business.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,4)")
                        .HasDefaultValue(0m);

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("StockBalance")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Code");

                    b.HasIndex("Description");

                    b.HasIndex("Code", "Reference");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("alter.treinamento.business.Models.Product", b =>
                {
                    b.HasOne("alter.treinamento.business.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .IsRequired();

                    b.OwnsOne("alter.treinamento.business.Models.Dimension", "Dimension", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Depth")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("decimal(18,4)")
                                .HasDefaultValue(0m)
                                .HasColumnName("Depth");

                            b1.Property<decimal>("Height")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("decimal(18,4)")
                                .HasDefaultValue(0m)
                                .HasColumnName("Height");

                            b1.Property<decimal>("Width")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("decimal(18,4)")
                                .HasDefaultValue(0m)
                                .HasColumnName("Width");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Category");

                    b.Navigation("Dimension");
                });

            modelBuilder.Entity("alter.treinamento.business.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
