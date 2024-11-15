﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LEGO.Inventory.Capacity.Planning.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20241110163130_AddTablesforRDC")]
    partial class AddTablesforRDC
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters.LocalDistributionCenter", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("finishedGoodsName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("finishedGoodsStockQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("regionalDistributionCenterName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("safetyStockQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("safetyStockThreshold")
                        .HasColumnType("INTEGER");

                    b.HasKey("name");

                    b.ToTable("LocalDistributionCenter");
                });

            modelBuilder.Entity("LEGO.Inventory.Capacity.Planning.Domain.DistributionCenters.RegionalDistributionCenter", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("FinishedGoodsName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FinishedGoodsStockQuantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.ToTable("RegionalDistributionCenter");
                });

            modelBuilder.Entity("LEGO.Inventory.Capacity.Planning.Domain.Orders.SalesOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FinishedGoodsName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LocalDistributionCenterName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SalesOrders");
                });

            modelBuilder.Entity("LEGO.Inventory.Capacity.Planning.Domain.StockTransportOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FinishedGoodsName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LocalDistributionCenterName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegionalDistributionCenterName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("StockTransportOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
