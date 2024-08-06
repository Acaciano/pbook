﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PBook.Infra;

#nullable disable

namespace PBook.Infra.Migrations
{
    [DbContext(typeof(BancoContent))]
    [Migration("20240806135354_InicialDB")]
    partial class InicialDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PBook.Domain.Entidades.Assunto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Assuntos");
                });

            modelBuilder.Entity("PBook.Domain.Entidades.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("PBook.Domain.Entidades.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnoPublicacao")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Edicao")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Editora")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal?>("Preco")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("PBook.Domain.Entidades.LivroAssunto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssuntoId")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssuntoId");

                    b.HasIndex("LivroId");

                    b.ToTable("LivroAssuntos");
                });

            modelBuilder.Entity("PBook.Domain.Entidades.LivroAutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("LivroId");

                    b.ToTable("LivroAutores");
                });

            modelBuilder.Entity("PBook.Domain.Entidades.LivroAssunto", b =>
                {
                    b.HasOne("PBook.Domain.Entidades.Assunto", "Assunto")
                        .WithMany()
                        .HasForeignKey("AssuntoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PBook.Domain.Entidades.Livro", "Livro")
                        .WithMany("Assuntos")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assunto");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("PBook.Domain.Entidades.LivroAutor", b =>
                {
                    b.HasOne("PBook.Domain.Entidades.Autor", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PBook.Domain.Entidades.Livro", "Livro")
                        .WithMany("Autores")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("PBook.Domain.Entidades.Livro", b =>
                {
                    b.Navigation("Assuntos");

                    b.Navigation("Autores");
                });
#pragma warning restore 612, 618
        }
    }
}
