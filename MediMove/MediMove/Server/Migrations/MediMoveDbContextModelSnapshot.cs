﻿// <auto-generated />
using System;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediMove.Server.Migrations
{
    [DbContext(typeof(MediMoveDbContext))]
    partial class MediMoveDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MediMove.Server.Entities.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<int>("ParamedicId")
                        .HasColumnType("int");

                    b.Property<int>("ShiftType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParamedicId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Billing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalInformationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInformationId");

                    b.ToTable("Billings");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Dispatcher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalInformationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInformationId");

                    b.ToTable("Dispatchers");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Paramedic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDriver")
                        .HasColumnType("bit");

                    b.Property<int>("PersonalInformationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInformationId");

                    b.ToTable("Paramedics");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PersonalInformationId")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInformationId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PersonalInformationId = 77,
                            Weight = 40
                        });
                });

            modelBuilder.Entity("MediMove.Server.Entities.PersonalInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApartmentNumber")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateProvince")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PersonalInformations");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ParamedicId")
                        .HasColumnType("int");

                    b.Property<decimal>("PayPerHour")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ParamedicId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DispatcherId")
                        .HasColumnType("int");

                    b.Property<decimal>("Income")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("DispatcherId");

                    b.ToTable("Salary");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<int>("ParamedicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("ParamedicId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Transport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BillingId")
                        .HasColumnType("int");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Financing")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("PatientPosition")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("TransportType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillingId");

                    b.HasIndex("PatientId");

                    b.HasIndex("TeamId");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Availability", b =>
                {
                    b.HasOne("MediMove.Server.Entities.Paramedic", "Paramedic")
                        .WithMany("Availabilities")
                        .HasForeignKey("ParamedicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paramedic");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Billing", b =>
                {
                    b.HasOne("MediMove.Server.Entities.PersonalInformation", "PersonalInformation")
                        .WithMany()
                        .HasForeignKey("PersonalInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Dispatcher", b =>
                {
                    b.HasOne("MediMove.Server.Entities.PersonalInformation", "PersonalInformation")
                        .WithMany()
                        .HasForeignKey("PersonalInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Paramedic", b =>
                {
                    b.HasOne("MediMove.Server.Entities.PersonalInformation", "PersonalInformation")
                        .WithMany()
                        .HasForeignKey("PersonalInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Patient", b =>
                {
                    b.HasOne("MediMove.Server.Entities.PersonalInformation", "PersonalInformation")
                        .WithMany()
                        .HasForeignKey("PersonalInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Rate", b =>
                {
                    b.HasOne("MediMove.Server.Entities.Paramedic", "Paramedic")
                        .WithMany("Rates")
                        .HasForeignKey("ParamedicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paramedic");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Salary", b =>
                {
                    b.HasOne("MediMove.Server.Entities.Dispatcher", "Dispatcher")
                        .WithMany("Salaries")
                        .HasForeignKey("DispatcherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dispatcher");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Team", b =>
                {
                    b.HasOne("MediMove.Server.Entities.Paramedic", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediMove.Server.Entities.Paramedic", "Paramedic")
                        .WithMany()
                        .HasForeignKey("ParamedicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Paramedic");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Transport", b =>
                {
                    b.HasOne("MediMove.Server.Entities.Billing", "Billing")
                        .WithMany()
                        .HasForeignKey("BillingId");

                    b.HasOne("MediMove.Server.Entities.Patient", "Patient")
                        .WithMany("Transports")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediMove.Server.Entities.Team", "Team")
                        .WithMany("Transports")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Billing");

                    b.Navigation("Patient");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Dispatcher", b =>
                {
                    b.Navigation("Salaries");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Paramedic", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("Rates");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Patient", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("MediMove.Server.Entities.Team", b =>
                {
                    b.Navigation("Transports");
                });
#pragma warning restore 612, 618
        }
    }
}
