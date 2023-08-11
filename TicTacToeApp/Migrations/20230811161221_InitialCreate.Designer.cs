﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicTacToeApp.Data;

#nullable disable

namespace TicTacToeApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230811161221_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("TicTacToeApp.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Board")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("GameState")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NextPlayer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Winner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
