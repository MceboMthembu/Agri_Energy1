﻿// <auto-generated />
using System;
using Agri_Energy1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Agri_Energy1.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240525180805_FP")]
    partial class FP
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Agri_Energy1.Models.FarmerProducts", b =>
                {
                    b.Property<int>("FarmerId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("FarmerId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("FarmerProducts");
                });

            modelBuilder.Entity("Agri_Energy1.Models.Farmers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Farmers");
                });

            modelBuilder.Entity("Agri_Energy1.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Agri_Energy1.Models.FarmerProducts", b =>
                {
                    b.HasOne("Agri_Energy1.Models.Farmers", "Farmers")
                        .WithMany("FarmerProducts")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Agri_Energy1.Models.Products", "Products")
                        .WithMany("FarmerProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Farmers");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Agri_Energy1.Models.Farmers", b =>
                {
                    b.Navigation("FarmerProducts");
                });

            modelBuilder.Entity("Agri_Energy1.Models.Products", b =>
                {
                    b.Navigation("FarmerProducts");
                });
#pragma warning restore 612, 618
        }
    }
}