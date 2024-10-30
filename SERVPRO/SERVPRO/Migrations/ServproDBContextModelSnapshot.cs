﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SERVPRO.Data;

#nullable disable

namespace SERVPRO.Migrations
{
    [DbContext(typeof(ServproDBContext))]
    partial class ServproDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SERVPRO.Models.Equipamento", b =>
                {
                    b.Property<string>("Serial")
                        .HasColumnType("text");

                    b.Property<string>("ClienteCPF")
                        .HasColumnType("text");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Serial");

                    b.HasIndex("ClienteCPF");

                    b.ToTable("Equipamentos");
                });

            modelBuilder.Entity("SERVPRO.Models.HistoricoOS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasMaxLength(255)
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("OrdemDeServicoId")
                        .HasColumnType("integer");

                    b.Property<string>("TecnicoCPF")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrdemDeServicoId");

                    b.HasIndex("TecnicoCPF");

                    b.ToTable("HistoricosOS");
                });

            modelBuilder.Entity("SERVPRO.Models.OrdemDeServico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClienteCPF")
                        .HasColumnType("text");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("IdProduto")
                        .HasColumnType("text");

                    b.Property<string>("MetodoPagamento")
                        .HasColumnType("text");

                    b.Property<string>("ProdutoIdProduto")
                        .HasColumnType("text");

                    b.Property<string>("SerialEquipamento")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TecnicoCPF")
                        .HasColumnType("text");

                    b.Property<string>("ValorTotal")
                        .HasColumnType("text");

                    b.Property<DateTime>("dataAbertura")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("dataConclusao")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClienteCPF");

                    b.HasIndex("ProdutoIdProduto");

                    b.HasIndex("SerialEquipamento");

                    b.HasIndex("TecnicoCPF");

                    b.ToTable("OrdensDeServico");
                });

            modelBuilder.Entity("SERVPRO.Models.Produto", b =>
                {
                    b.Property<string>("IdProduto")
                        .HasColumnType("text");

                    b.Property<string>("CategoriaProduto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DescricaoProduto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PrecoProduto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("QtdProduto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdProduto");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("SERVPRO.Models.Usuario", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<string>("TipoUsuario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CPF");

                    b.ToTable("Usuarios", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SERVPRO.Models.Administrador", b =>
                {
                    b.HasBaseType("SERVPRO.Models.Usuario");

                    b.Property<DateTime>("DataContratacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Departamento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .HasColumnType("text");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("SERVPRO.Models.Cliente", b =>
                {
                    b.HasBaseType("SERVPRO.Models.Usuario");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("SERVPRO.Models.Tecnico", b =>
                {
                    b.HasBaseType("SERVPRO.Models.Usuario");

                    b.Property<string>("Especialidade")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.ToTable("Tecnicos", (string)null);
                });

            modelBuilder.Entity("SERVPRO.Models.Equipamento", b =>
                {
                    b.HasOne("SERVPRO.Models.Cliente", "Cliente")
                        .WithMany("Equipamentos")
                        .HasForeignKey("ClienteCPF")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("SERVPRO.Models.HistoricoOS", b =>
                {
                    b.HasOne("SERVPRO.Models.OrdemDeServico", "OrdemDeServico")
                        .WithMany("Historicos")
                        .HasForeignKey("OrdemDeServicoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SERVPRO.Models.Tecnico", "Tecnico")
                        .WithMany()
                        .HasForeignKey("TecnicoCPF")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("OrdemDeServico");

                    b.Navigation("Tecnico");
                });

            modelBuilder.Entity("SERVPRO.Models.OrdemDeServico", b =>
                {
                    b.HasOne("SERVPRO.Models.Cliente", "Cliente")
                        .WithMany("OrdensDeServico")
                        .HasForeignKey("ClienteCPF")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SERVPRO.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoIdProduto");

                    b.HasOne("SERVPRO.Models.Equipamento", "Equipamento")
                        .WithMany()
                        .HasForeignKey("SerialEquipamento")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SERVPRO.Models.Tecnico", "Tecnico")
                        .WithMany("OrdensDeServico")
                        .HasForeignKey("TecnicoCPF")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Cliente");

                    b.Navigation("Equipamento");

                    b.Navigation("Produto");

                    b.Navigation("Tecnico");
                });

            modelBuilder.Entity("SERVPRO.Models.Cliente", b =>
                {
                    b.HasOne("SERVPRO.Models.Usuario", null)
                        .WithOne()
                        .HasForeignKey("SERVPRO.Models.Cliente", "CPF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SERVPRO.Models.Tecnico", b =>
                {
                    b.HasOne("SERVPRO.Models.Usuario", null)
                        .WithOne()
                        .HasForeignKey("SERVPRO.Models.Tecnico", "CPF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SERVPRO.Models.OrdemDeServico", b =>
                {
                    b.Navigation("Historicos");
                });

            modelBuilder.Entity("SERVPRO.Models.Cliente", b =>
                {
                    b.Navigation("Equipamentos");

                    b.Navigation("OrdensDeServico");
                });

            modelBuilder.Entity("SERVPRO.Models.Tecnico", b =>
                {
                    b.Navigation("OrdensDeServico");
                });
#pragma warning restore 612, 618
        }
    }
}
