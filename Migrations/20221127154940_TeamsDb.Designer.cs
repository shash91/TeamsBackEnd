﻿// <auto-generated />
using DatabaseClient.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace TeamsBackEnd.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221127154940_TeamsDb")]
    partial class TeamsDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamsBackEnd.Models.UserDetails", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Email");

                    b.ToTable("UserDetails");
                });
#pragma warning restore 612, 618
        }
    }
}