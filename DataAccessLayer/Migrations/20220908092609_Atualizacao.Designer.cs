﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(DataBaseDbContext))]
    [Migration("20220908092609_Atualizacao")]
    partial class Atualizacao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entities.Bairro", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CidadeId")
                        .HasColumnType("int");

                    b.Property<string>("NomeBairro")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CidadeId");

                    b.ToTable("BAIRROS", (string)null);
                });

            modelBuilder.Entity("Entities.Cargo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Funcao")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(900)");

                    b.Property<int>("NivelPermissao")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Funcao")
                        .IsUnique();

                    b.ToTable("CARGOS", (string)null);
                });

            modelBuilder.Entity("Entities.Cidade", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<string>("NomeCidade")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EstadoId");

                    b.ToTable("CIDADES", (string)null);
                });

            modelBuilder.Entity("Entities.Compromisso", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("ID");

                    b.ToTable("COMPROMISSO", (string)null);
                });

            modelBuilder.Entity("Entities.Endereco", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("BairroID")
                        .HasColumnType("int");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Complemento")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("NumeroCasa")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BairroID");

                    b.ToTable("ENDERECOS", (string)null);
                });

            modelBuilder.Entity("Entities.Equipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("EQUIPES", (string)null);
                });

            modelBuilder.Entity("Entities.EquipeFuncionario", b =>
                {
                    b.Property<int>("EquipeID")
                        .HasColumnType("int");

                    b.Property<int>("FuncionarioID")
                        .HasColumnType("int");

                    b.HasKey("EquipeID", "FuncionarioID");

                    b.HasIndex("FuncionarioID");

                    b.ToTable("EQUIPE_FUNCIONARIO", (string)null);
                });

            modelBuilder.Entity("Entities.Estado", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("NomeEstado")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(900)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)");

                    b.HasKey("ID");

                    b.HasIndex("NomeEstado")
                        .IsUnique();

                    b.HasIndex("Sigla")
                        .IsUnique();

                    b.ToTable("ESTADOS", (string)null);
                });

            modelBuilder.Entity("Entities.Funcionario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CargoID")
                        .HasColumnType("int");

                    b.Property<string>("Celular")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(17)
                        .IsUnicode(false)
                        .HasColumnType("varchar(17)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("EnderecoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("EstadoCivil")
                        .HasColumnType("int");

                    b.Property<string>("Foto")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("Genero")
                        .HasColumnType("int");

                    b.Property<bool>("HasRequiredTest")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsAtivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsFirstLogin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<double>("Salario")
                        .HasColumnType("float");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CargoID");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasDatabaseName("UQ_FUNCIONARIO_CPF");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("UQ_FUNCIONARIO_EMAIL");

                    b.HasIndex("EnderecoID");

                    b.ToTable("FUNCIONARIOS", (string)null);
                });

            modelBuilder.Entity("Entities.Bairro", b =>
                {
                    b.HasOne("Entities.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cidade");
                });

            modelBuilder.Entity("Entities.Cidade", b =>
                {
                    b.HasOne("Entities.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("Entities.Endereco", b =>
                {
                    b.HasOne("Entities.Bairro", "Bairro")
                        .WithMany()
                        .HasForeignKey("BairroID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bairro");
                });

            modelBuilder.Entity("Entities.EquipeFuncionario", b =>
                {
                    b.HasOne("Entities.Equipe", "Equipe")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EquipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Funcionario", "Funcionario")
                        .WithMany("Equipes")
                        .HasForeignKey("FuncionarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Entities.Funcionario", b =>
                {
                    b.HasOne("Entities.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Entities.Equipe", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("Entities.Funcionario", b =>
                {
                    b.Navigation("Equipes");
                });
#pragma warning restore 612, 618
        }
    }
}
