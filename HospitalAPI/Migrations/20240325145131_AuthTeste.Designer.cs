﻿// <auto-generated />
using System;
using HospitalAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HospitalAPI.Migrations
{
    [DbContext(typeof(HospitalAPIContext))]
    [Migration("20240325145131_AuthTeste")]
    partial class AuthTeste
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HospitalAPI.Models.ConsultaModel", b =>
                {
                    b.Property<Guid>("ConsultaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataDoCadastro")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ConsultaId");

                    b.HasIndex("MedicoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Consultas");
                });

            modelBuilder.Entity("HospitalAPI.Models.ConvenioModel", b =>
                {
                    b.Property<int>("ConvenioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConvenioId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ConvenioId");

                    b.ToTable("Convenios");
                });

            modelBuilder.Entity("HospitalAPI.Models.MedicoModel", b =>
                {
                    b.Property<int>("MedicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicoId"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("ConsultaId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("MedicoId");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("HospitalAPI.Models.PacienteModel", b =>
                {
                    b.Property<int>("PacienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PacienteId"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("ConsultaId")
                        .HasColumnType("int");

                    b.Property<int?>("ConvenioId")
                        .HasColumnType("int");

                    b.Property<string>("DataDeNascimento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("TemConvenio")
                        .HasColumnType("bit");

                    b.HasKey("PacienteId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("HospitalAPI.Models.RetornoModel", b =>
                {
                    b.Property<int>("RetornoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RetornoId"));

                    b.Property<Guid>("ConsultaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("RetornoId");

                    b.ToTable("Retornos");
                });

            modelBuilder.Entity("HospitalAPI.Models.ConsultaModel", b =>
                {
                    b.HasOne("HospitalAPI.Models.MedicoModel", "Medico")
                        .WithMany("Consulta")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalAPI.Models.PacienteModel", "Paciente")
                        .WithMany("Consulta")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("HospitalAPI.Models.MedicoModel", b =>
                {
                    b.Navigation("Consulta");
                });

            modelBuilder.Entity("HospitalAPI.Models.PacienteModel", b =>
                {
                    b.Navigation("Consulta");
                });
#pragma warning restore 612, 618
        }
    }
}
