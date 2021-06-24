﻿// <auto-generated />
using System;
using IDSTORE2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IDSTORE2.Migrations
{
    [DbContext(typeof(APIContext))]
    [Migration("20210624104606_UpdateDB_5")]
    partial class UpdateDB_5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FileTag", b =>
                {
                    b.Property<Guid>("FilesFileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("int");

                    b.HasKey("FilesFileId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("FileTag");
                });

            modelBuilder.Entity("IDSTORE2.Models.File", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FileId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("IDSTORE2.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeLogID")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogId");

                    b.HasIndex("TypeLogID");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("IDSTORE2.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("IDSTORE2.Models.TypeLog", b =>
                {
                    b.Property<int>("TypeLogId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeLogId");

                    b.ToTable("TypeLog");
                });

            modelBuilder.Entity("FileTag", b =>
                {
                    b.HasOne("IDSTORE2.Models.File", null)
                        .WithMany()
                        .HasForeignKey("FilesFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IDSTORE2.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IDSTORE2.Models.Log", b =>
                {
                    b.HasOne("IDSTORE2.Models.TypeLog", "TypeLog")
                        .WithMany("Log")
                        .HasForeignKey("TypeLogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeLog");
                });

            modelBuilder.Entity("IDSTORE2.Models.TypeLog", b =>
                {
                    b.Navigation("Log");
                });
#pragma warning restore 612, 618
        }
    }
}
