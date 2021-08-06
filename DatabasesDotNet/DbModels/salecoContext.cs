using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DatabasesDotNet.DbModels
{
    public partial class salecoContext : DbContext
    {
        public salecoContext()
        {
        }

        public salecoContext(DbContextOptions<salecoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=saleco;password=Nikita", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CusCode)
                    .HasName("PRIMARY");

                entity.ToTable("customer");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.CusCode)
                    .ValueGeneratedNever()
                    .HasColumnName("Cus_code");

                entity.Property(e => e.CusAreaCode).HasColumnName("Cus_AreaCode");

                entity.Property(e => e.CusBalance).HasColumnName("Cus_Balance");

                entity.Property(e => e.CusFName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Cus_fName");

                entity.Property(e => e.CusInitial)
                    .HasMaxLength(8)
                    .HasColumnName("Cus_Initial");

                entity.Property(e => e.CusLName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Cus_lName");

                entity.Property(e => e.CusPhone)
                    .HasMaxLength(45)
                    .HasColumnName("Cus_phone");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.InvNumber)
                    .HasName("PRIMARY");

                entity.ToTable("invoice");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.CusCode, "fk_Invoice_Customer1_idx");

                entity.Property(e => e.InvNumber)
                    .ValueGeneratedNever()
                    .HasColumnName("Inv_Number");

                entity.Property(e => e.CusCode).HasColumnName("Cus_code");

                entity.Property(e => e.InvDate)
                    .HasColumnType("date")
                    .HasColumnName("Inv_Date");

                entity.HasOne(d => d.CusCodeNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CusCode)
                    .HasConstraintName("fk_Invoice_Customer1");
            });

            modelBuilder.Entity<Line>(entity =>
            {
                entity.HasKey(e => new { e.LineNumber, e.InvNumber })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("line");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.InvNumber, "fk_Line_Invoice1_idx");

                entity.HasIndex(e => e.PCode, "fk_Line_Product1_idx");

                entity.Property(e => e.InvNumber).HasColumnName("Inv_Number");

                entity.Property(e => e.LinePrice).HasColumnName("Line_Price");

                entity.Property(e => e.LineUnits).HasColumnName("Line_Units");

                entity.Property(e => e.PCode)
                    .HasMaxLength(45)
                    .HasColumnName("P_code");

                entity.HasOne(d => d.InvNumberNavigation)
                    .WithMany(p => p.Lines)
                    .HasForeignKey(d => d.InvNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Line_Invoice1");

                entity.HasOne(d => d.PCodeNavigation)
                    .WithMany(p => p.Lines)
                    .HasForeignKey(d => d.PCode)
                    .HasConstraintName("fk_Line_Product1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.PCode)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.VCode, "fk_Product_Vendor1_idx");

                entity.Property(e => e.PCode)
                    .HasMaxLength(45)
                    .HasColumnName("P_code");

                entity.Property(e => e.PDescript)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("P_descript");

                entity.Property(e => e.PDiscount).HasColumnName("P_Discount");

                entity.Property(e => e.PInDate)
                    .HasColumnType("date")
                    .HasColumnName("P_InDate");

                entity.Property(e => e.PMin).HasColumnName("P_Min");

                entity.Property(e => e.PPrice).HasColumnName("P_Price");

                entity.Property(e => e.PQoh).HasColumnName("P_QOH");

                entity.Property(e => e.VCode).HasColumnName("V_code");

                entity.HasOne(d => d.VCodeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.VCode)
                    .HasConstraintName("fk_Product_Vendor1");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.VCode)
                    .HasName("PRIMARY");

                entity.ToTable("vendor");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.VCode)
                    .ValueGeneratedNever()
                    .HasColumnName("V_code");

                entity.Property(e => e.VAreaCode).HasColumnName("V_AreaCode");

                entity.Property(e => e.VContact)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("V_contact");

                entity.Property(e => e.VName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("V_name");

                entity.Property(e => e.VOrder)
                    .HasMaxLength(45)
                    .HasColumnName("V_order");

                entity.Property(e => e.VPhone)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("V_phone");

                entity.Property(e => e.VState)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("V_state");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
