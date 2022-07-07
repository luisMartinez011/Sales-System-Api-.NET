using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ventas.Models
{
    public partial class dotnetventasContext : DbContext
    {
        public dotnetventasContext()
        {
        }

        public dotnetventasContext(DbContextOptions<dotnetventasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Concepto> Conceptos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Concepto>(entity =>
            {
                entity.HasKey(e => e.IdVentas)
                    .HasName("concepto_pkey");

                entity.ToTable("concepto");

                entity.Property(e => e.IdVentas)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_ventas");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IdProducto)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_producto");

                entity.Property(e => e.Importe).HasColumnName("importe");

                entity.Property(e => e.PrecioUnitario).HasColumnName("precio_unitario");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Conceptos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("concepto_productos_fkey");

                entity.HasOne(d => d.IdVentasNavigation)
                    .WithOne(p => p.Concepto)
                    .HasForeignKey<Concepto>(d => d.IdVentas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("concepto_ventas_fkey");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsers)
                    .HasName("usuarios_pkey");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsers)
                    .HasColumnName("id_users")
                    .HasDefaultValueSql("nextval('usuarios_id_seq'::regclass)");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("C");

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("ventas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Ventas_id_seq\"'::regclass)");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_cliente");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("ventas_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
