﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreApp;

namespace StoreApp.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20201229040206_adding price to order details")]
    partial class addingpricetoorderdetails
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("StoreApp.Customer", b =>
                {
                    b.Property<Guid>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CustomerAge")
                        .HasColumnType("int");

                    b.Property<string>("CustomerFName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerLName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PerferedStoreStoreLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.HasKey("CustomerID");

                    b.HasIndex("PerferedStoreStoreLocationId");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("StoreApp.Inventory", b =>
                {
                    b.Property<Guid>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("StoreLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InventoryId");

                    b.HasIndex("ProductID");

                    b.HasIndex("StoreLocationId");

                    b.ToTable("inventories");
                });

            modelBuilder.Entity("StoreApp.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("isCart")
                        .HasColumnType("bit");

                    b.Property<bool>("isOrdered")
                        .HasColumnType("bit");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerID");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("StoreApp.OrderDetails", b =>
                {
                    b.Property<Guid>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ItemInventoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("QuantityOrdered")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("ItemInventoryId");

                    b.HasIndex("OrderId");

                    b.ToTable("orderDetails");
                });

            modelBuilder.Entity("StoreApp.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAgeRestricted")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductID");

                    b.ToTable("products");
                });

            modelBuilder.Entity("StoreApp.StoreLocation", b =>
                {
                    b.Property<Guid>("StoreLocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StoreLocationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreLocationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreLocationId");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("StoreApp.Customer", b =>
                {
                    b.HasOne("StoreApp.StoreLocation", "PerferedStore")
                        .WithMany("FrequentCustomers")
                        .HasForeignKey("PerferedStoreStoreLocationId");

                    b.Navigation("PerferedStore");
                });

            modelBuilder.Entity("StoreApp.Inventory", b =>
                {
                    b.HasOne("StoreApp.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("StoreApp.StoreLocation", "StoreLocation")
                        .WithMany("Inventory")
                        .HasForeignKey("StoreLocationId");

                    b.Navigation("Product");

                    b.Navigation("StoreLocation");
                });

            modelBuilder.Entity("StoreApp.Order", b =>
                {
                    b.HasOne("StoreApp.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("StoreApp.OrderDetails", b =>
                {
                    b.HasOne("StoreApp.Inventory", "Item")
                        .WithMany()
                        .HasForeignKey("ItemInventoryId");

                    b.HasOne("StoreApp.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("StoreApp.StoreLocation", b =>
                {
                    b.Navigation("FrequentCustomers");

                    b.Navigation("Inventory");
                });
#pragma warning restore 612, 618
        }
    }
}
