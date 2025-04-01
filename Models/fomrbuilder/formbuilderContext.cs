using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class formbuilderContext : DbContext
    {
        public formbuilderContext()
        {
        }

        public formbuilderContext(DbContextOptions<formbuilderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<ConfigForm> ConfigForms { get; set; }
        public virtual DbSet<DoubleType> DoubleTypes { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<Funcionalidade> Funcionalidades { get; set; }
        public virtual DbSet<IntegerType> IntegerTypes { get; set; }
        public virtual DbSet<Permiso> Permisos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StringType> StringTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=168.226.219.57,2424; Initial Catalog=formbuilder; User ID=caucete; Password=Caucete123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.IdAnswer)
                    .HasName("PK__Answer__CDB2012F043FF789");

                entity.ToTable("Answer");

                entity.Property(e => e.IdAnswer).HasColumnName("Id_Answer");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.FechaEliminacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_eliminacion");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdConfigForm).HasColumnName("Id_ConfigForm");

                entity.Property(e => e.IdField).HasColumnName("Id_Field");

                entity.Property(e => e.IdentificadorFila).HasColumnName("identificador_fila");

                entity.Property(e => e.Valor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("valor");

                entity.HasOne(d => d.IdConfigFormNavigation)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.IdConfigForm)
                    .HasConstraintName("FK__Answer__Id_Confi__29572725");

                entity.HasOne(d => d.IdFieldNavigation)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.IdField)
                    .HasConstraintName("FK__Answer__Id_Field__2A4B4B5E");
            });

            modelBuilder.Entity<ConfigForm>(entity =>
            {
                entity.HasKey(e => e.IdConfigForm)
                    .HasName("PK__ConfigFo__57E8BE3B0DD766B7");

                entity.ToTable("ConfigForm");

                entity.Property(e => e.IdConfigForm).HasColumnName("Id_ConfigForm");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.FechaEliminacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_eliminacion");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("titulo");
            });

            modelBuilder.Entity<DoubleType>(entity =>
            {
                entity.HasKey(e => e.DoubleId)
                    .HasName("PK__DoubleTy__039A351B22095EB6");

                entity.ToTable("DoubleType");

                entity.Property(e => e.DoubleId).HasColumnName("double_id");

                entity.Property(e => e.Anulado).HasColumnName("anulado");

                entity.Property(e => e.AssumedValue).HasColumnName("assumed_value");

                entity.Property(e => e.DefaultValue).HasColumnName("default_value");

                entity.Property(e => e.MaxConfiguration)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("max_configuration")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaxValue).HasColumnName("max_value");

                entity.Property(e => e.MinConfiguration)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("min_configuration")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MinValue).HasColumnName("min_value");

                entity.Property(e => e.ValueList)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("value_list")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.HasKey(e => e.IdField)
                    .HasName("PK__Field__864FBBF895666114");

                entity.ToTable("Field");

                entity.Property(e => e.IdField).HasColumnName("Id_Field");

                entity.Property(e => e.Clase)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("clase");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Etiqueta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("etiqueta");

                entity.Property(e => e.FechaEliminacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_eliminacion");

                entity.Property(e => e.IdConfigForm).HasColumnName("Id_ConfigForm");

                entity.Property(e => e.Marcador)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("marcador");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Opciones)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("opciones");

                entity.Property(e => e.Orden).HasColumnName("orden");

                entity.Property(e => e.Requerido).HasColumnName("requerido");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo");

                entity.Property(e => e.Visible).HasColumnName("visible");

                entity.HasOne(d => d.IdConfigFormNavigation)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.IdConfigForm)
                    .HasConstraintName("FK__Field__Id_Config__267ABA7A");
            });

            modelBuilder.Entity<Funcionalidade>(entity =>
            {
                entity.ToTable("funcionalidades");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<IntegerType>(entity =>
            {
                entity.HasKey(e => e.IntegerId)
                    .HasName("PK__IntegerT__5D02F428F0B43067");

                entity.ToTable("IntegerType");

                entity.Property(e => e.IntegerId).HasColumnName("integer_id");

                entity.Property(e => e.Anulado).HasColumnName("anulado");

                entity.Property(e => e.AssumedValue).HasColumnName("assumed_value");

                entity.Property(e => e.DefaultValue).HasColumnName("default_value");

                entity.Property(e => e.MaxConfiguration)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("max_configuration")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaxValue).HasColumnName("max_value");

                entity.Property(e => e.MinConfiguration)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("min_configuration")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MinValue).HasColumnName("min_value");

                entity.Property(e => e.ValueList)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("value_list")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.ToTable("permisos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PermisoId).HasColumnName("permisoId");

                entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");

                entity.HasOne(d => d.PermisoNavigation)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.PermisoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__permisos__permis__6477ECF3");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__permisos__permis__6383C8BA");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RolId)
                    .HasName("PK__Roles__F92302D1DE449CEB");

                entity.Property(e => e.RolId)
                    .ValueGeneratedNever()
                    .HasColumnName("RolID");

                entity.Property(e => e.RolName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StringType>(entity =>
            {
                entity.HasKey(e => e.StringId)
                    .HasName("PK__StringTy__BB2650DCD25AC7A5");

                entity.ToTable("StringType");

                entity.Property(e => e.StringId).HasColumnName("string_id");

                entity.Property(e => e.Anulado).HasColumnName("anulado");

                entity.Property(e => e.AssumedValue)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("assumed_value")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultValue)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("default_value")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaskLibrary)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("mask_library")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ValueList)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("value_list")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FechaEliminacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_eliminacion");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleID__5535A963");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
