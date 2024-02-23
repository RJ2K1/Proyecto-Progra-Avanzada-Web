using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities;

public partial class ProyectoWebContext : DbContext
{
    public ProyectoWebContext()
    {
    }

    public ProyectoWebContext(DbContextOptions<ProyectoWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditoria> Auditoria { get; set; }

    public virtual DbSet<Departamentos> Departamentos { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=r03x;Database=ProyectoWeb;Integrated Security=True;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auditori__3213E83F70B1739D");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("accion");
            entity.Property(e => e.FechaAccion)
                .HasColumnType("datetime")
                .HasColumnName("fechaAccion");
            entity.Property(e => e.RegistroId).HasColumnName("registroId");
            entity.Property(e => e.TablaAfectada)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tablaAfectada");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__usuar__6A30C649");
        });

        modelBuilder.Entity<Departamentos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3213E83FD284D46A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83F4405CA47");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombreRol");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3213E83F403516E1");

            entity.ToTable("Ticket");

            entity.HasIndex(e => e.NumeroTicket, "UQ__Ticket__C4DA2FE3C4D43A8C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AsignadoAusuarioId).HasColumnName("AsignadoAUsuarioId");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Complejidad)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaDeCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroTicket)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("numeroTicket");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.AsignadoAusuario).WithMany(p => p.TicketAsignadoAusuarios)
                .HasForeignKey(d => d.AsignadoAusuarioId)
                .HasConstraintName("FK__Ticket__Asignado__6754599E");

            entity.HasOne(d => d.CreadoPorUsuario).WithMany(p => p.TicketCreadoPorUsuarios)
                .HasForeignKey(d => d.CreadoPorUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__CreadoPo__66603565");

            entity.HasOne(d => d.DepartamentoAsignado).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.DepartamentoAsignadoId)
                .HasConstraintName("FK__Ticket__Departam__656C112C");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83FFBF52D21");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__AB6E6164A6BDEA7E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.DepartamentoId).HasColumnName("departamentoId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RolId).HasColumnName("rolId");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.DepartamentoId)
                .HasConstraintName("FK__Usuarios__depart__619B8048");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__rolId__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
