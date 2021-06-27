using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class FinalProjectContext : DbContext
    {
        public FinalProjectContext()
        {
        }

        public FinalProjectContext(DbContextOptions<FinalProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Cabin> Cabins { get; set; }
        public virtual DbSet<Citizen> Citizens { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Managerxcabin> Managerxcabins { get; set; }
        public virtual DbSet<Municipality> Municipalities { get; set; }
        public virtual DbSet<SideEffect> SideEffects { get; set; }
        public virtual DbSet<TypeCitizen> TypeCitizens { get; set; }
        public virtual DbSet<TypeEmployee> TypeEmployees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=FinalProject;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.IdAppointment)
                    .HasName("PK__APPOINTM__F9CC20B7F238CBA2");

                entity.ToTable("APPOINTMENT");

                entity.Property(e => e.IdAppointment).HasColumnName("id_appointment");

                entity.Property(e => e.DateAppointment)
                    .HasColumnType("date")
                    .HasColumnName("date_appointment");

                entity.Property(e => e.HourAppointment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hour_appointment");

                entity.Property(e => e.HourArrived)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hour_arrived");

                entity.Property(e => e.HourVaccunated)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hour_vaccunated");

                entity.Property(e => e.IdCabin).HasColumnName("id_cabin");

                entity.Property(e => e.IdCitizen).HasColumnName("id_citizen");

                entity.HasOne(d => d.IdCabinNavigation)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.IdCabin)
                    .HasConstraintName("FK_CABIN_APPOINTMENT");

                entity.HasOne(d => d.IdCitizenNavigation)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.IdCitizen)
                    .HasConstraintName("FK_CITIZEN_APPOINTMENT");
            });

            modelBuilder.Entity<Cabin>(entity =>
            {
                entity.ToTable("CABIN");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Addres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("addres");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdDepartment).HasColumnName("id_department");

                entity.Property(e => e.IdMunicipality).HasColumnName("id_municipality");

                entity.Property(e => e.ManagerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("manager_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.Cabins)
                    .HasForeignKey(d => d.IdDepartment)
                    .HasConstraintName("FK_DEPARTMENT_CABIN");

                entity.HasOne(d => d.IdMunicipalityNavigation)
                    .WithMany(p => p.Cabins)
                    .HasForeignKey(d => d.IdMunicipality)
                    .HasConstraintName("FK_MUNICIPALITY_CABIN");
            });

            modelBuilder.Entity<Citizen>(entity =>
            {
                entity.HasKey(e => e.Dui)
                    .HasName("PK__CITIZEN__D876F1BE5C66CB29");

                entity.ToTable("CITIZEN");

                entity.Property(e => e.Dui)
                    .ValueGeneratedNever()
                    .HasColumnName("dui");

                entity.Property(e => e.Addres)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("addres");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.IdDepartment).HasColumnName("id_department");

                entity.Property(e => e.IdMunicipality).HasColumnName("id_municipality");

                entity.Property(e => e.IdTypeCitizen).HasColumnName("id_type_citizen");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.Citizens)
                    .HasForeignKey(d => d.IdDepartment)
                    .HasConstraintName("FK_DEPARTMENT_CITIZEN");

                entity.HasOne(d => d.IdMunicipalityNavigation)
                    .WithMany(p => p.Citizens)
                    .HasForeignKey(d => d.IdMunicipality)
                    .HasConstraintName("FK_MUNICIPALITY_CITIZEN");

                entity.HasOne(d => d.IdTypeCitizenNavigation)
                    .WithMany(p => p.Citizens)
                    .HasForeignKey(d => d.IdTypeCitizen)
                    .HasConstraintName("FK_TYPEcITIZEN_CITIZEN");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Department1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("department");
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.ToTable("DISEASES");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Diseases)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("diseases");

                entity.Property(e => e.DuiCitizen).HasColumnName("dui_citizen");

                entity.HasOne(d => d.DuiCitizenNavigation)
                    .WithMany(p => p.Diseases)
                    .HasForeignKey(d => d.DuiCitizen)
                    .HasConstraintName("FK_CITIZEN_DISEASES");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Addres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("addres");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.IdManager).HasColumnName("id_manager");

                entity.Property(e => e.IdTypeEmployee).HasColumnName("id_type_employee");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdManager)
                    .HasConstraintName("FK_MANAGER_EMPLOYEE");

                entity.HasOne(d => d.IdTypeEmployeeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdTypeEmployee)
                    .HasConstraintName("FK_TYPE_EMPLOYEE_EMPLOYEE");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("MANAGER");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PasswordManager)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password_manager");

                entity.Property(e => e.UserEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_employee");
            });

            modelBuilder.Entity<Managerxcabin>(entity =>
            {
                entity.HasKey(e => e.IdRegister)
                    .HasName("PK__MANAGERX__4815B9940CD52EF3");

                entity.ToTable("MANAGERXCABIN");

                entity.Property(e => e.IdRegister).HasColumnName("id_register");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("date_time");

                entity.Property(e => e.IdCabin).HasColumnName("id_cabin");

                entity.Property(e => e.IdManager).HasColumnName("id_manager");

                entity.HasOne(d => d.IdCabinNavigation)
                    .WithMany(p => p.Managerxcabins)
                    .HasForeignKey(d => d.IdCabin)
                    .HasConstraintName("FK_CABIN_MANAGERXCABIN");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.Managerxcabins)
                    .HasForeignKey(d => d.IdManager)
                    .HasConstraintName("FK_MANAGER_MANAGERXCABIN");
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.ToTable("MUNICIPALITY");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDepartment).HasColumnName("id_department");

                entity.Property(e => e.Municipality1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("municipality");

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.Municipalities)
                    .HasForeignKey(d => d.IdDepartment)
                    .HasConstraintName("FK_DEPARTMENT_MUNICIPALITY");
            });

            modelBuilder.Entity<SideEffect>(entity =>
            {
                entity.ToTable("SIDE_EFFECTS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DuiCitizen).HasColumnName("dui_citizen");

                entity.Property(e => e.SideEffect1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("side_effect");

                entity.HasOne(d => d.DuiCitizenNavigation)
                    .WithMany(p => p.SideEffects)
                    .HasForeignKey(d => d.DuiCitizen)
                    .HasConstraintName("FK_CITIZEN_SIDEEFFECTS");
            });

            modelBuilder.Entity<TypeCitizen>(entity =>
            {
                entity.ToTable("TYPE_CITIZEN");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TypeCitizen1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("type_citizen");
            });

            modelBuilder.Entity<TypeEmployee>(entity =>
            {
                entity.ToTable("TYPE_EMPLOYEE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Employee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("employee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
