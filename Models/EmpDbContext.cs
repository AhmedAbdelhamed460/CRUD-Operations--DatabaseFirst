using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDatabaseFirst.Models;

public partial class EmpDbContext : DbContext
{
    public EmpDbContext()
    {
    }

    public EmpDbContext(DbContextOptions<EmpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City__B4BEB8BE0F92403F");

            entity.ToTable("City");

            entity.Property(e => e.CityId).HasColumnName("cityID");
            entity.Property(e => e.CityNameArabic)
                .HasMaxLength(250)
                .HasColumnName("cityNameArabic");
            entity.Property(e => e.CityNameEnglish)
                .HasMaxLength(250)
                .HasColumnName("cityNameEnglish");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD673A616E");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentAbbr).HasMaxLength(250);
            entity.Property(e => e.DepartmentName).HasMaxLength(250);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1845393BF");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.CityId).HasColumnName("cityID");
            entity.Property(e => e.Dbo)
                .HasColumnType("date")
                .HasColumnName("DBO");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.EmployeeNameEnglish).IsUnicode(false);
            entity.Property(e => e.HiringDate)
                .HasColumnType("date")
                .HasColumnName("HiringDATE");
            entity.Property(e => e.Salary).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.City).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Employee__cityID__3B75D760");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employee__Depart__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
