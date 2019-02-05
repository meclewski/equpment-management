﻿// <auto-generated />
using System;
using Measuring_equipment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Measuring_equipment.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190204094728_PlacesDepts")]
    partial class PlacesDepts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Measuring_equipment.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentDesc");

                    b.Property<string>("DepartmentName");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Measuring_equipment.Models.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CurrentlyInUse");

                    b.Property<string>("DeviceDesc");

                    b.Property<string>("InventoryNo");

                    b.Property<DateTime?>("ProductionDate");

                    b.Property<int>("RegistrationNo");

                    b.Property<string>("SerialNo");

                    b.Property<DateTime?>("TimeToVerification");

                    b.Property<int>("TypeId");

                    b.Property<DateTime?>("VerificationDate");

                    b.Property<string>("VerificationResult");

                    b.HasKey("DeviceId");

                    b.HasIndex("TypeId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Measuring_equipment.Models.Laboratory", b =>
                {
                    b.Property<int>("LaboratoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Accreditation");

                    b.Property<string>("LaboratoryDesc");

                    b.Property<string>("LaboratoryName");

                    b.HasKey("LaboratoryId");

                    b.ToTable("Laboratories");
                });

            modelBuilder.Entity("Measuring_equipment.Models.Place", b =>
                {
                    b.Property<int>("PlaceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentId");

                    b.Property<string>("PlaceDesc");

                    b.Property<string>("PlaceName");

                    b.HasKey("PlaceId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("Measuring_equipment.Models.Producer", b =>
                {
                    b.Property<int>("ProducerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProducerDesc");

                    b.Property<string>("ProducerName");

                    b.HasKey("ProducerId");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("Measuring_equipment.Models.Type", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeviceName");

                    b.Property<byte[]>("Image");

                    b.Property<int>("LaboratoryId");

                    b.Property<decimal>("Price");

                    b.Property<int>("ProducerId");

                    b.Property<string>("TypeDesc");

                    b.Property<string>("TypeName");

                    b.Property<int>("ValidityPierod");

                    b.Property<int>("VerificationId");

                    b.HasKey("TypeId");

                    b.HasIndex("LaboratoryId");

                    b.HasIndex("ProducerId");

                    b.HasIndex("VerificationId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Measuring_equipment.Models.Verification", b =>
                {
                    b.Property<int>("VerificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("VerificationDesc");

                    b.Property<string>("VerificationName");

                    b.HasKey("VerificationId");

                    b.ToTable("Verification");
                });

            modelBuilder.Entity("Measuring_equipment.Models.Device", b =>
                {
                    b.HasOne("Measuring_equipment.Models.Type", "Type")
                        .WithMany("Devices")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Measuring_equipment.Models.Type", b =>
                {
                    b.HasOne("Measuring_equipment.Models.Laboratory", "Laboratory")
                        .WithMany("Types")
                        .HasForeignKey("LaboratoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Measuring_equipment.Models.Producer", "Producer")
                        .WithMany("Types")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Measuring_equipment.Models.Verification", "Verification")
                        .WithMany("Types")
                        .HasForeignKey("VerificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
