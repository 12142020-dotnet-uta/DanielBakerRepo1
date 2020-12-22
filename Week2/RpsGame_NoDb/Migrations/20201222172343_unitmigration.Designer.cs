﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RpsGame_NoDb;

namespace RpsGame_NoDb.Migrations
{
    [DbContext(typeof(RpsDbContext))]
    [Migration("20201222172343_unitmigration")]
    partial class unitmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("RpsGame_NoDb.Match", b =>
                {
                    b.Property<Guid>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Player1PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Player2PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MatchId");

                    b.HasIndex("Player1PlayerId");

                    b.HasIndex("Player2PlayerId");

                    b.ToTable("matches");
                });

            modelBuilder.Entity("RpsGame_NoDb.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Fname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.ToTable("players");
                });

            modelBuilder.Entity("RpsGame_NoDb.Round", b =>
                {
                    b.Property<Guid>("RoundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Player1Choice")
                        .HasColumnType("int");

                    b.Property<int>("Player2Choice")
                        .HasColumnType("int");

                    b.Property<Guid?>("WinningPlayerPlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoundId");

                    b.HasIndex("WinningPlayerPlayerId");

                    b.ToTable("rounds");
                });

            modelBuilder.Entity("RpsGame_NoDb.Match", b =>
                {
                    b.HasOne("RpsGame_NoDb.Player", "Player1")
                        .WithMany()
                        .HasForeignKey("Player1PlayerId");

                    b.HasOne("RpsGame_NoDb.Player", "Player2")
                        .WithMany()
                        .HasForeignKey("Player2PlayerId");

                    b.Navigation("Player1");

                    b.Navigation("Player2");
                });

            modelBuilder.Entity("RpsGame_NoDb.Round", b =>
                {
                    b.HasOne("RpsGame_NoDb.Player", "WinningPlayer")
                        .WithMany()
                        .HasForeignKey("WinningPlayerPlayerId");

                    b.Navigation("WinningPlayer");
                });
#pragma warning restore 612, 618
        }
    }
}
