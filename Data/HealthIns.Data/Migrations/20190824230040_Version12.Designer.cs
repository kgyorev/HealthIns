﻿// <auto-generated />
using System;
using HealthIns.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HealthIns.Data.Migrations
{
    [DbContext(typeof(HealthInsDbContext))]
    [Migration("20190824230040_Version12")]
    partial class Version12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HealthIns.Data.Models.Bussines.Contract", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<DateTime>("CreationDate");

                    b.Property<long?>("DistributorId");

                    b.Property<int>("Duration");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Frequency");

                    b.Property<DateTime>("NextBillingDueDate");

                    b.Property<long?>("PersonId");

                    b.Property<double>("PremiumAmount");

                    b.Property<long?>("ProductId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId");

                    b.HasIndex("PersonId");

                    b.HasIndex("ProductId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Bussines.Distributor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName");

                    b.Property<long?>("OrganizationId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("Distributors");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Bussines.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FrequencyRule");

                    b.Property<string>("Idntfr");

                    b.Property<string>("Label");

                    b.Property<int>("MaxAge");

                    b.Property<int>("MinAge");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Financial.ClaimActivity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ClaimDate");

                    b.Property<string>("ClaimEventType");

                    b.Property<long?>("ContractId");

                    b.Property<double>("OperationAmount");

                    b.Property<DateTime>("RecordDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("ClaimActivities");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Financial.MoneyIn", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ContractId");

                    b.Property<double>("OperationAmount");

                    b.Property<DateTime>("RecordDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("MoneyIns");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Financial.Premium", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ContractId");

                    b.Property<DateTime>("EndDate");

                    b.Property<long?>("MoneyInId");

                    b.Property<double>("OperationAmount");

                    b.Property<DateTime>("RecordDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("MoneyInId")
                        .IsUnique()
                        .HasFilter("[MoneyInId] IS NOT NULL");

                    b.ToTable("Premiums");
                });

            modelBuilder.Entity("HealthIns.Data.Models.HealthInsUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HealthIns.Data.Models.PrsnOrg.Organization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FullName");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Vat");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("HealthIns.Data.Models.PrsnOrg.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Egn");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FullName");

                    b.Property<string>("Sex");

                    b.Property<bool>("Smoker");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Bussines.Contract", b =>
                {
                    b.HasOne("HealthIns.Data.Models.Bussines.Distributor", "Distributor")
                        .WithMany()
                        .HasForeignKey("DistributorId");

                    b.HasOne("HealthIns.Data.Models.PrsnOrg.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.HasOne("HealthIns.Data.Models.Bussines.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Bussines.Distributor", b =>
                {
                    b.HasOne("HealthIns.Data.Models.PrsnOrg.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("HealthIns.Data.Models.HealthInsUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Financial.ClaimActivity", b =>
                {
                    b.HasOne("HealthIns.Data.Models.Bussines.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Financial.MoneyIn", b =>
                {
                    b.HasOne("HealthIns.Data.Models.Bussines.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId");
                });

            modelBuilder.Entity("HealthIns.Data.Models.Financial.Premium", b =>
                {
                    b.HasOne("HealthIns.Data.Models.Bussines.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId");

                    b.HasOne("HealthIns.Data.Models.Financial.MoneyIn", "MoneyIn")
                        .WithOne("Premium")
                        .HasForeignKey("HealthIns.Data.Models.Financial.Premium", "MoneyInId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HealthIns.Data.Models.HealthInsUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HealthIns.Data.Models.HealthInsUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HealthIns.Data.Models.HealthInsUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HealthIns.Data.Models.HealthInsUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
