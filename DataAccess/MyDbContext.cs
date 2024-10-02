using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderEntry> OrderEntries { get; set; }
        public virtual DbSet<PaperProperty> PaperProperties { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>

            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasKey(e => e.Id).HasName("customers_pkey");
            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasKey(e => e.Id).HasName("orders_pkey");

                entity.Property(e => e.OrderDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Status).HasDefaultValueSql("'pending'::character varying");

                entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("orders_customer_id_fkey");
            });

            // Configure OrderEntry entity
            modelBuilder.Entity<OrderEntry>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasKey(e => e.Id).HasName("order_entries_pkey");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderEntries)
                    .HasConstraintName("order_entries_order_id_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderEntries)
                    .HasConstraintName("order_entries_product_id_fkey");
            });

            // Configure Paper entity
            modelBuilder.Entity<Paper>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasKey(e => e.Id).HasName("paper_pkey");

                entity.Property(e => e.Discontinued).HasDefaultValue(false);
                entity.Property(e => e.Stock).HasDefaultValue(0);

                // Many-to-many relationship between Paper and Property via PaperProperty
                entity.HasMany(d => d.Properties)
                    .WithMany(p => p.Papers)
                    .UsingEntity<PaperProperty>(
                        j => j.HasOne(pp => pp.Property)
                              .WithMany(p => p.PaperProperties)
                              .HasForeignKey(pp => pp.PropertyId)
                              .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("paper_properties_property_id_fkey"),
                        j => j.HasOne(pp => pp.Paper)
                              .WithMany(p => p.PaperProperties)
                              .HasForeignKey(pp => pp.PaperId)
                              .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("paper_properties_paper_id_fkey"),
                        j =>
                        {
                            j.HasKey(pp => new { pp.PaperId, pp.PropertyId }).HasName("paper_properties_pkey");
                            j.ToTable("paper_properties");
                            j.HasIndex(pp => pp.PropertyId).HasDatabaseName("IX_paper_properties_property_id");
                        });
            });

            // Configure Property entity
            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasKey(e => e.Id).HasName("properties_pkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}