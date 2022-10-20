﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrialBalanceWebApp.Data;

#nullable disable

namespace TrialBalanceWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221020184757_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BankAccount")
                        .HasColumnType("integer");

                    b.Property<int>("ClassId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.AccountClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BankId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("AccountClasses");
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Balance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<double>("Active")
                        .HasColumnType("double precision");

                    b.Property<double>("Passive")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Revenue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<double>("Credit")
                        .HasColumnType("double precision");

                    b.Property<double>("Debit")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Revenues");
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Account", b =>
                {
                    b.HasOne("TrialBalanceWebApp.Entities.AccountClass", null)
                        .WithMany("Accounts")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.AccountClass", b =>
                {
                    b.HasOne("TrialBalanceWebApp.Entities.Bank", null)
                        .WithMany("AccountClasses")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Balance", b =>
                {
                    b.HasOne("TrialBalanceWebApp.Entities.Account", "Account")
                        .WithOne("OpeningBalance")
                        .HasForeignKey("TrialBalanceWebApp.Entities.Balance", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Revenue", b =>
                {
                    b.HasOne("TrialBalanceWebApp.Entities.Account", "Account")
                        .WithOne("Revenue")
                        .HasForeignKey("TrialBalanceWebApp.Entities.Revenue", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Account", b =>
                {
                    b.Navigation("OpeningBalance")
                        .IsRequired();

                    b.Navigation("Revenue")
                        .IsRequired();
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.AccountClass", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("TrialBalanceWebApp.Entities.Bank", b =>
                {
                    b.Navigation("AccountClasses");
                });
#pragma warning restore 612, 618
        }
    }
}