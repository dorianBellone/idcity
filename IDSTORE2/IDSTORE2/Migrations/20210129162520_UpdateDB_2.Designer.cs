﻿// <auto-generated />
using System;
using IDSTORE2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IDSTORE2.Migrations
{
    [DbContext(typeof(APIContext))]
    [Migration("20210129162520_UpdateDB_2")]
    partial class UpdateDB_2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("IDSTORE2.Models.File", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("FileId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("IDSTORE2.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("TagId");

                    b.HasIndex("FileId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("IDSTORE2.Models.Tag", b =>
                {
                    b.HasOne("IDSTORE2.Models.File", null)
                        .WithMany("Tags")
                        .HasForeignKey("FileId");
                });

            modelBuilder.Entity("IDSTORE2.Models.File", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
