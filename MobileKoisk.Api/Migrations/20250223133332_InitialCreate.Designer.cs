﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobileKoisk.Api.Data;

#nullable disable

namespace MobileKoisk.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250223133332_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("MobileKoisk.Api.Models.Basket", b =>
                {
                    b.Property<int>("BasketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BasketId");

                    b.HasIndex("UserId");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.BasketItem", b =>
                {
                    b.Property<string>("Barcode")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageSource")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReceiptId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Barcode");

                    b.HasIndex("ReceiptId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRead")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.Product", b =>
                {
                    b.Property<string>("barcode")
                        .HasColumnType("TEXT");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("from_date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("image_url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("item_description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("item_num")
                        .HasColumnType("INTEGER");

                    b.Property<double>("quantity")
                        .HasColumnType("REAL");

                    b.Property<int>("section")
                        .HasColumnType("INTEGER");

                    b.Property<double>("selling_price")
                        .HasColumnType("REAL");

                    b.HasKey("barcode");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReceiptNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("REAL");

                    b.Property<int>("TransactionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.ReceiptItem", b =>
                {
                    b.Property<int>("ReceiptItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("PriceAtPurchase")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReceiptItemId");

                    b.ToTable("ReceiptItems");
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.UserData", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastLoginAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.Basket", b =>
                {
                    b.HasOne("MobileKoisk.Api.Models.UserData", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.BasketItem", b =>
                {
                    b.HasOne("MobileKoisk.Api.Models.Receipt", null)
                        .WithMany("PurchasedItems")
                        .HasForeignKey("ReceiptId");
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.Notification", b =>
                {
                    b.HasOne("MobileKoisk.Api.Models.UserData", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.Receipt", b =>
                {
                    b.HasOne("MobileKoisk.Api.Models.UserData", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobileKoisk.Api.Models.Receipt", b =>
                {
                    b.Navigation("PurchasedItems");
                });
#pragma warning restore 612, 618
        }
    }
}
