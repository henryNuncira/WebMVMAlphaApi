using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiPruebaAlpha.Models
{
    public  class AlphaMVMContext : DbContext
    {
        public AlphaMVMContext()
        {
        }

        public AlphaMVMContext(DbContextOptions<AlphaMVMContext> options)
            : base(options)
        {
        }

        public DbSet<ClasificadoRadicado> ClasificadoRadicados { get; set; }
        public DbSet<ContactoPersona> ContactoPersonas { get; set; }
        public DbSet<RadicadoCorrespondecium> RadicadoCorrespondecia { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5LDDOSM;Database=AlphaMVM;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<ClasificadoRadicado>(entity =>
            {
                entity.HasKey(e => e.IdClasificadoR);

                entity.ToTable("ClasificadoRadicado");

                entity.Property(e => e.IdClasificadoR).HasColumnName("idClasificadoR");

                entity.Property(e => e.SerialRadicado)
                    .HasMaxLength(50)
                    .HasColumnName("serialRadicado");

                entity.Property(e => e.TipoRadicado).HasColumnName("tipoRadicado");
            });

            modelBuilder.Entity<ContactoPersona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("ContactoPersona");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .HasColumnName("nit");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoContacto).HasColumnName("tipoContacto");
            });

            modelBuilder.Entity<RadicadoCorrespondecium>(entity =>
            {
                entity.HasKey(e => e.IdRadicado);

                entity.Property(e => e.IdRadicado).HasColumnName("idRadicado");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdClasificadoR).HasColumnName("idClasificadoR");

                entity.Property(e => e.IdPersonaContacto).HasColumnName("idPersonaContacto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.RespaldoCorrespondencia).HasColumnName("respaldoCorrespondencia");

                modelBuilder.Entity<RadicadoCorrespondecium>()
                    .HasOne(d => d.IdClasificadoRNavigation)
                    .WithMany(p => p.RadicadoCorrespondecia)
                    .HasForeignKey(d => d.IdClasificadoR);

                entity.HasOne(d => d.IdPersonaContactoNavigation)
                    .WithMany(p => p.RadicadoCorrespondecia)
                    .HasForeignKey(d => d.IdPersonaContacto)
                    .HasConstraintName("FK__RadicadoC__idPer__412EB0B6");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .HasColumnName("clave");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.Dni)
                    .HasMaxLength(50)
                    .HasColumnName("dni");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.IdRadicado).HasColumnName("idRadicado");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Salt)
                    .HasMaxLength(500)
                    .HasColumnName("salt");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .HasColumnName("token");

                modelBuilder.Entity<Usuario>()
                .HasOne<Rol>(d => d.IdRolNavigation)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol);

                modelBuilder.Entity<Usuario>()
                    .HasOne(d => d.IdRadicadoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRadicado);
             
            });

          
        }

      
    }
}
