using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NFCCheckApp
{
    public partial class NFC_Check_DBContext : DbContext
    {
        public virtual DbSet<Binding> Binding { get; set; }
        public virtual DbSet<ElmahError> ElmahError { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Gate> Gate { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Tool> Tool { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VQ3KM31;Initial Catalog=NFC_Check_DB;Persist Security Info=True;User ID=MMate;Password=Asdf123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Binding>(entity =>
            {
                entity.Property(e => e.BindingId).HasColumnName("Binding_ID");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.ToolId).HasColumnName("Tool_ID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Binding)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Binding_Employee1");

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.Binding)
                    .HasForeignKey(d => d.ToolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Binding_Tool1");
            });

            modelBuilder.Entity<ElmahError>(entity =>
            {
                entity.HasKey(e => e.ErrorId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ELMAH_Error");

                entity.HasIndex(e => new { e.Application, e.TimeUtc, e.Sequence })
                    .HasName("IX_ELMAH_Error_App_Time_Seq");

                entity.Property(e => e.ErrorId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AllXml)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Application)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Sequence).ValueGeneratedOnAdd();

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.TimeUtc).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.BadgeId).HasColumnName("Badge_ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.It).HasColumnName("IT");

                entity.Property(e => e.Rank)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gate>(entity =>
            {
                entity.Property(e => e.GateId).HasColumnName("Gate_ID");

                entity.Property(e => e.GateInstruction)
                    .HasColumnName("Gate_Instruction")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GateNumber).HasColumnName("Gate_Number");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasIndex(e => e.GateId)
                    .HasName("NonClusteredIndex-20171026-165339");

                entity.Property(e => e.LogId).HasColumnName("Log_ID");

                entity.Property(e => e.BindigId).HasColumnName("Bindig_ID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GateId).HasColumnName("Gate_ID");

                entity.Property(e => e.Time).HasColumnType("date");

                entity.HasOne(d => d.Bindig)
                    .WithMany(p => p.Log)
                    .HasForeignKey(d => d.BindigId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Log_Binding1");

                entity.HasOne(d => d.Gate)
                    .WithMany(p => p.Log)
                    .HasForeignKey(d => d.GateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Log_Gate");
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.Property(e => e.ToolId).HasColumnName("Tool_ID");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NfcId).HasColumnName("NFC_ID");

                entity.Property(e => e.SerialNumber).HasColumnName("Serial_Number");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
