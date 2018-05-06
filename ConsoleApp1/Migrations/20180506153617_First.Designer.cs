﻿// <auto-generated />
using System;
using ConsoleApp1.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp1.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20180506153617_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConsoleApp1.Domain.ArchivalCategoty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Years");

                    b.HasKey("Id");

                    b.ToTable("ArchivalCategoty");
                });

            modelBuilder.Entity("ConsoleApp1.Domain.Box", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsSealed");

                    b.HasKey("Id");

                    b.ToTable("Boxes");
                });

            modelBuilder.Entity("ConsoleApp1.Domain.BoxDocumentationTypeInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BoxId");

                    b.Property<int?>("TypeId");

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.HasIndex("TypeId");

                    b.ToTable("BoxDocumentationTypeInfo");
                });

            modelBuilder.Entity("ConsoleApp1.Domain.DocumentationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArchivalCategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ArchivalCategoryId");

                    b.ToTable("DocumentationType");
                });

            modelBuilder.Entity("ConsoleApp1.Domain.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("ConsoleApp1.Domain.PackageDocumentationTypeInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PackageId");

                    b.Property<int?>("TypeId");

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("TypeId");

                    b.ToTable("PackageDocumentationTypeInfo");
                });

            modelBuilder.Entity("ConsoleApp1.Domain.Box", b =>
                {
                    b.OwnsOne("ConsoleApp1.Domain.BoxStatus", "Status", b1 =>
                        {
                            b1.Property<int?>("BoxId");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("Status");

                            b1.ToTable("Boxes");

                            b1.HasOne("ConsoleApp1.Domain.Box")
                                .WithOne("Status")
                                .HasForeignKey("ConsoleApp1.Domain.BoxStatus", "BoxId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ConsoleApp1.Domain.CidNumber", "Cid", b1 =>
                        {
                            b1.Property<int?>("BoxId");

                            b1.Property<string>("Value")
                                .HasColumnName("Cid");

                            b1.ToTable("Boxes");

                            b1.HasOne("ConsoleApp1.Domain.Box")
                                .WithOne("Cid")
                                .HasForeignKey("ConsoleApp1.Domain.CidNumber", "BoxId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ConsoleApp1.Domain.BoxDocumentationTypeInfo", b =>
                {
                    b.HasOne("ConsoleApp1.Domain.Box")
                        .WithMany("DocumentationTypeInfos")
                        .HasForeignKey("BoxId");

                    b.HasOne("ConsoleApp1.Domain.DocumentationType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("ConsoleApp1.Domain.DocumentationType", b =>
                {
                    b.HasOne("ConsoleApp1.Domain.ArchivalCategoty", "ArchivalCategory")
                        .WithMany()
                        .HasForeignKey("ArchivalCategoryId");
                });

            modelBuilder.Entity("ConsoleApp1.Domain.Package", b =>
                {
                    b.OwnsOne("ConsoleApp1.Domain.PackageNumber", "Number", b1 =>
                        {
                            b1.Property<int?>("PackageId");

                            b1.Property<string>("Value")
                                .HasColumnName("Number");

                            b1.ToTable("Packages");

                            b1.HasOne("ConsoleApp1.Domain.Package")
                                .WithOne("Number")
                                .HasForeignKey("ConsoleApp1.Domain.PackageNumber", "PackageId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ConsoleApp1.Domain.PackageStatus", "Status", b1 =>
                        {
                            b1.Property<int?>("PackageId");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("Status");

                            b1.ToTable("Packages");

                            b1.HasOne("ConsoleApp1.Domain.Package")
                                .WithOne("Status")
                                .HasForeignKey("ConsoleApp1.Domain.PackageStatus", "PackageId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ConsoleApp1.Domain.PackageDocumentationTypeInfo", b =>
                {
                    b.HasOne("ConsoleApp1.Domain.Package")
                        .WithMany("DocumentationTypeInfos")
                        .HasForeignKey("PackageId");

                    b.HasOne("ConsoleApp1.Domain.DocumentationType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
