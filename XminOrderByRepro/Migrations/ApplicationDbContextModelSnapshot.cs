﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using XminOrderByRepro.Data;

#nullable disable

namespace XminOrderByRepro.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("XminOrderByRepro.Data.Bar", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("FooId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FooId");

                    b.ToTable("Bar");
                });

            modelBuilder.Entity("XminOrderByRepro.Data.Foo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Foo");
                });

            modelBuilder.Entity("XminOrderByRepro.Data.Bar", b =>
                {
                    b.HasOne("XminOrderByRepro.Data.Foo", "Foo")
                        .WithMany()
                        .HasForeignKey("FooId");

                    b.Navigation("Foo");
                });
#pragma warning restore 612, 618
        }
    }
}