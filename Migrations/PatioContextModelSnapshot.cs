﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using patioAPI.Models;

#nullable disable

namespace patioAPI.Migrations
{
    [DbContext(typeof(PatioContext))]
    partial class PatioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("patioAPI.Models.Filial", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BranchId"));

                    b.Property<string>("Address")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Branch")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Cidade")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Estado")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("BranchId");

                    b.ToTable("filiais", "RM99208");
                });

            modelBuilder.Entity("patioAPI.Models.Moto", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Court")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Localization")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("VehicleId");

                    b.ToTable("fiapmottu", "RM99208");
                });

            modelBuilder.Entity("patioAPI.Models.Patio", b =>
                {
                    b.Property<int>("CourtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourtId"));

                    b.Property<double>("AreaTotal")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("BranchId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("CourtLocal")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("GridCols")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("GridRows")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int?>("MaxMotos")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("CourtId");

                    b.ToTable("patios", "RM99208");
                });

            modelBuilder.Entity("patioAPI.Models.VeiculoPatio", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<int>("BranchId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("CourtId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("X")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Y")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("VehicleId");

                    b.ToTable("veiculopatio", "RM99208");
                });
#pragma warning restore 612, 618
        }
    }
}
