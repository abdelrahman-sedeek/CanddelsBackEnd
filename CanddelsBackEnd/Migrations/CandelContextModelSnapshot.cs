﻿// <auto-generated />
using System;
using CanddelsBackEnd.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    [DbContext(typeof(CandelContext))]
    partial class CandelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CanddelsBackEnd.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductVariantId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("CustomProductId")
                        .IsUnique()
                        .HasFilter("[CustomProductId] IS NOT NULL");

                    b.HasIndex("ProductVariantId")
                        .IsUnique()
                        .HasFilter("[ProductVariantId] IS NOT NULL");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.CustomProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Scent1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scent2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scent3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scent4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("customProducts");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShippingDetailId")
                        .HasColumnType("int");

                    b.Property<decimal>("SubTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ShippingDetailId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("customProductId")
                        .HasColumnType("int");

                    b.Property<int?>("productVariantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("customProductId")
                        .IsUnique()
                        .HasFilter("[customProductId] IS NOT NULL");

                    b.HasIndex("productVariantId")
                        .IsUnique()
                        .HasFilter("[productVariantId] IS NOT NULL");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Benfits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CalltoAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Features")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBestSeller")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDailyOffer")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.ProductVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Barcode")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PriceAfterDiscount")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariants");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.ShippingDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShippingDetails");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.CartItem", b =>
                {
                    b.HasOne("CanddelsBackEnd.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CanddelsBackEnd.Models.CustomProduct", "CustomProduct")
                        .WithOne("CartItem")
                        .HasForeignKey("CanddelsBackEnd.Models.CartItem", "CustomProductId");

                    b.HasOne("CanddelsBackEnd.Models.ProductVariant", "ProductVariant")
                        .WithOne("CartItem")
                        .HasForeignKey("CanddelsBackEnd.Models.CartItem", "ProductVariantId");

                    b.Navigation("Cart");

                    b.Navigation("CustomProduct");

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Order", b =>
                {
                    b.HasOne("CanddelsBackEnd.Models.ShippingDetail", "ShippingDetail")
                        .WithMany()
                        .HasForeignKey("ShippingDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShippingDetail");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.OrderItem", b =>
                {
                    b.HasOne("CanddelsBackEnd.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CanddelsBackEnd.Models.CustomProduct", "customProduct")
                        .WithOne("OrderItem")
                        .HasForeignKey("CanddelsBackEnd.Models.OrderItem", "customProductId");

                    b.HasOne("CanddelsBackEnd.Models.ProductVariant", "productVariant")
                        .WithOne("OrderItem")
                        .HasForeignKey("CanddelsBackEnd.Models.OrderItem", "productVariantId");

                    b.Navigation("Order");

                    b.Navigation("customProduct");

                    b.Navigation("productVariant");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Product", b =>
                {
                    b.HasOne("CanddelsBackEnd.Models.Category", "Category")
                        .WithMany("products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.ProductVariant", b =>
                {
                    b.HasOne("CanddelsBackEnd.Models.Product", "Product")
                        .WithMany("productVariants")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Category", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.CustomProduct", b =>
                {
                    b.Navigation("CartItem")
                        .IsRequired();

                    b.Navigation("OrderItem")
                        .IsRequired();
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.Product", b =>
                {
                    b.Navigation("productVariants");
                });

            modelBuilder.Entity("CanddelsBackEnd.Models.ProductVariant", b =>
                {
                    b.Navigation("CartItem")
                        .IsRequired();

                    b.Navigation("OrderItem")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
