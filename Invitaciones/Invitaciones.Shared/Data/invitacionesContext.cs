using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Invitaciones.Shared.Data
{
    public partial class InvitacionesContext : DbContext
    {
        public InvitacionesContext()
        {
        }

        public InvitacionesContext(DbContextOptions<InvitacionesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<EventoArchivo> EventoArchivos { get; set; }
        public virtual DbSet<EventoCategoria> EventoCategorias { get; set; }
        public virtual DbSet<EventoFecha> EventoFechas { get; set; }
        public virtual DbSet<EventoInvitacione> EventoInvitaciones { get; set; }
        public virtual DbSet<EventoInvitacionesHistory> EventoInvitacionesHistories { get; set; }
        public virtual DbSet<EventoInvitado> EventoInvitados { get; set; }
        public virtual DbSet<EventoInvitadosHistory> EventoInvitadosHistories { get; set; }
        public virtual DbSet<EventoOrganizadore> EventoOrganizadores { get; set; }
        public virtual DbSet<EventoTipo> EventoTipos { get; set; }
        public virtual DbSet<Grupo> Grupos { get; set; }
        public virtual DbSet<Institucione> Instituciones { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuUsuariosTipo> MenuUsuariosTipos { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuariosTipo> UsuariosTipos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-67MNR1G\\SQLEXPRESS2019;Database=Invitaciones;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento);

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("EventosHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdEvento).HasColumnName("id_Evento");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(1000)
                    .HasColumnName("direccion");

                entity.Property(e => e.EnlaceVirtual)
                    .HasMaxLength(500)
                    .HasColumnName("enlace_virtual");

                entity.Property(e => e.Evento1)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("evento");

                entity.Property(e => e.FechaConfirmacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Confirmacion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Creacion");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.Formato)
                    .HasMaxLength(150)
                    .HasColumnName("formato");

                entity.Property(e => e.HoraFin)
                    .HasMaxLength(10)
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraInicio)
                    .HasMaxLength(10)
                    .HasColumnName("hora_inicio");

                entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");

                entity.Property(e => e.IdOrganizador).HasColumnName("id_Organizador");

                entity.Property(e => e.IdTipo).HasColumnName("id_Tipo");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(250)
                    .HasColumnName("imagen");

                entity.Property(e => e.Latitud)
                    .HasMaxLength(150)
                    .HasColumnName("latitud");

                entity.Property(e => e.LimitePersonas).HasColumnName("limite_Personas");

                entity.Property(e => e.Longitud)
                    .HasMaxLength(150)
                    .HasColumnName("longitud");

                entity.Property(e => e.Lugar)
                    .HasMaxLength(1000)
                    .HasColumnName("lugar");

                entity.Property(e => e.Repertir)
                    .HasMaxLength(1)
                    .HasColumnName("repertir");

                entity.Property(e => e.UltimaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("ultima_Modificacion");

                entity.Property(e => e.ZonaHoraria)
                    .HasMaxLength(250)
                    .HasColumnName("zona_Horaria");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Eventos_Evento_Categorias");

                entity.HasOne(d => d.IdOrganizadorNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdOrganizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Eventos_Evento_Organizadores");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK_Eventos_Evento_Tipos");
            });

            modelBuilder.Entity<EventoArchivo>(entity =>
            {
                entity.HasKey(e => e.IdEventoArchivo);

                entity.ToTable("Evento_Archivos");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("Evento_ArchivosHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdEventoArchivo).HasColumnName("id_Evento_archivo");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Archivo)
                    .IsRequired()
                    .HasMaxLength(350)
                    .HasColumnName("archivo");

                entity.Property(e => e.IdEvento).HasColumnName("id_Evento");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("tipo")
                    .IsFixedLength();

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .HasColumnName("url");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.EventoArchivos)
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK_Evento_Archivos_Eventos");
            });

            modelBuilder.Entity<EventoCategoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.ToTable("Evento_Categorias");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("Evento_CategoriasHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Creacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("nombre");

                entity.Property(e => e.UltimaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("ultima_Modificacion");
            });

            modelBuilder.Entity<EventoFecha>(entity =>
            {
                entity.HasKey(e => e.IdEventoFechas);

                entity.ToTable("Evento_Fechas");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("Evento_FechasHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdEventoFechas).HasColumnName("id_Evento_Fechas");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FechaDesde)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_desde");

                entity.Property(e => e.FechaHasta)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_hasta");

                entity.Property(e => e.HoraDesde)
                    .HasMaxLength(50)
                    .HasColumnName("hora_desde");

                entity.Property(e => e.HoraHasta)
                    .HasMaxLength(50)
                    .HasColumnName("hora_hasta");

                entity.Property(e => e.IdEvento).HasColumnName("id_Evento");

                entity.Property(e => e.TodoDia).HasColumnName("todo_dia");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.EventoFechas)
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK_Evento_Fechas_Eventos");
            });

            modelBuilder.Entity<EventoInvitacione>(entity =>
            {
                entity.HasKey(e => e.IdInvitacion);

                entity.ToTable("Evento_Invitaciones");

                entity.Property(e => e.IdInvitacion).HasColumnName("id_Invitacion");

                entity.Property(e => e.Archivo).HasMaxLength(500);

                entity.Property(e => e.Direccion).HasMaxLength(2500);

                entity.Property(e => e.Fecha).HasMaxLength(500);

                entity.Property(e => e.FechaEnvio)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Envio");

                entity.Property(e => e.IdEvento).HasColumnName("id_Evento");

                entity.Property(e => e.Lugar).HasMaxLength(1000);

                entity.Property(e => e.Tenida).HasMaxLength(1000);

                entity.Property(e => e.TextConf)
                    .HasMaxLength(2000)
                    .HasColumnName("Text_Conf");

                entity.Property(e => e.TipoConf)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo_Conf");

                entity.Property(e => e.Titulo).HasMaxLength(500);

                entity.Property(e => e.Token).HasMaxLength(500);

                //entity.Property(e => e.ValidFrom).HasDefaultValueSql("(sysutcdatetime())");

                //entity.Property(e => e.ValidTo).HasDefaultValueSql("(CONVERT([datetime2],'9999-12-31 23:59:59.9999999'))");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.EventoInvitaciones)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento_Invitaciones_Eventos");
            });

            modelBuilder.Entity<EventoInvitacionesHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Evento_InvitacionesHistory");

                entity.HasIndex(e => new { e.ValidTo, e.ValidFrom }, "ix_Evento_InvitacionesHistory")
                    .IsClustered();

                entity.Property(e => e.IdEvento).HasColumnName("id_Evento");

                entity.Property(e => e.IdInvitacion).HasColumnName("id_Invitacion");
            });

            modelBuilder.Entity<EventoInvitado>(entity =>
            {
                entity.HasKey(e => e.IdEventoParticipante)
                    .HasName("PK_Evento_Participantes");

                entity.ToTable("Evento_Invitados");

                entity.Property(e => e.IdEventoParticipante).HasColumnName("id_Evento_Participante");

                entity.Property(e => e.Confirmado)
                    .HasColumnName("confirmado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaConfirmacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Confirmacion");

                entity.Property(e => e.IdEventoInvitacion).HasColumnName("Id_Evento_Invitacion");

                entity.Property(e => e.IdInstitucion).HasColumnName("id_Institucion");

                entity.Property(e => e.IdPersona).HasColumnName("id_Persona");

                entity.Property(e => e.TextoAdicional)
                    .HasMaxLength(500)
                    .HasColumnName("texto_adicional");

                entity.Property(e => e.Token).HasMaxLength(150);

                //entity.Property(e => e.ValidFrom).HasDefaultValueSql("(sysutcdatetime())");

                //entity.Property(e => e.ValidTo).HasDefaultValueSql("(CONVERT([datetime2],'9999-12-31 23:59:59.9999999'))");

                entity.HasOne(d => d.IdEventoInvitacionNavigation)
                    .WithMany(p => p.EventoInvitados)
                    .HasForeignKey(d => d.IdEventoInvitacion)
                    .HasConstraintName("FK_Evento_Invitados_Evento_Invitaciones");

                entity.HasOne(d => d.IdInstitucionNavigation)
                    .WithMany(p => p.EventoInvitados)
                    .HasForeignKey(d => d.IdInstitucion)
                    .HasConstraintName("FK_Evento_Invitados_Instituciones");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.EventoInvitados)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK_Evento_Invitados_Personas");
            });

            modelBuilder.Entity<EventoInvitadosHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Evento_InvitadosHistory");

                entity.HasIndex(e => new { e.ValidTo, e.ValidFrom }, "ix_Evento_InvitadosHistory")
                    .IsClustered();

                entity.Property(e => e.Cargo)
                    .HasMaxLength(50)
                    .HasColumnName("cargo");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.IdEvento).HasColumnName("id_Evento");

                entity.Property(e => e.IdEventoParticipante).HasColumnName("id_Evento_Participante");

                entity.Property(e => e.IdInstitucion).HasColumnName("id_Institucion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(800)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<EventoOrganizadore>(entity =>
            {
                entity.HasKey(e => e.IdOrganizador)
                    .HasName("PK_Organizadores");

                entity.ToTable("Evento_Organizadores");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("Evento_OrganizadoresHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdOrganizador).HasColumnName("id_Organizador");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Creacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("nombre");

                entity.Property(e => e.UltimaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("ultima_Modificacion");
            });

            modelBuilder.Entity<EventoTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo);

                entity.ToTable("Evento_Tipos");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("Evento_TiposHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdTipo).HasColumnName("id_Tipo");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Creacion");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("tipo");

                entity.Property(e => e.UltimaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("ultima_Modificacion");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo);

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("GruposHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdGrupo).HasColumnName("id_Grupo");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.IdInstitucion).HasColumnName("id_Institucion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdInstitucionNavigation)
                    .WithMany(p => p.Grupos)
                    .HasForeignKey(d => d.IdInstitucion)
                    .HasConstraintName("FK_Grupos_Instituciones");
            });

            modelBuilder.Entity<Institucione>(entity =>
            {
                entity.HasKey(e => e.IdInstitucion);

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("InstitucionesHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdInstitucion).HasColumnName("id_Institucion");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Action).HasMaxLength(255);

                entity.Property(e => e.Callsite).HasMaxLength(255);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Ipaddress).HasMaxLength(150);

                entity.Property(e => e.Level).HasMaxLength(10);

                entity.Property(e => e.Logger).HasMaxLength(255);

                entity.Property(e => e.ReferrerUrl).HasMaxLength(255);

                entity.Property(e => e.RequestUrl).HasMaxLength(255);

                entity.Property(e => e.Userid).HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu);

                entity.ToTable("menu");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("menuHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdMenu).HasColumnName("id_Menu");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.IdMenuSuperior).HasColumnName("id_MenuSuperior");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("link");

                entity.Property(e => e.MenuTitulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("menu_Titulo");

                entity.Property(e => e.Orden).HasColumnName("orden");
            });

            modelBuilder.Entity<MenuUsuariosTipo>(entity =>
            {
                entity.HasKey(e => e.IdMenuTipoUsuario);

                entity.ToTable("menu_usuarios_tipos");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("menu_usuarios_tiposHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdMenuTipoUsuario).HasColumnName("id_MenuTipoUsuario");

                entity.Property(e => e.IdMenu).HasColumnName("id_Menu");

                entity.Property(e => e.IdUsuarioTipo).HasColumnName("id_UsuarioTipo");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.MenuUsuariosTipos)
                    .HasForeignKey(d => d.IdMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_menu_usuarios_tipos_menu");

                entity.HasOne(d => d.IdUsuarioTipoNavigation)
                    .WithMany(p => p.MenuUsuariosTipos)
                    .HasForeignKey(d => d.IdUsuarioTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_menu_usuarios_tipos_usuarios_tipos");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdInvitado);

                entity.Property(e => e.IdInvitado).HasColumnName("id_Invitado");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cargo)
                    .HasMaxLength(100)
                    .HasColumnName("cargo");

                entity.Property(e => e.Celular)
                    .HasMaxLength(50)
                    .HasColumnName("celular");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.IdGrupo).HasColumnName("id_Grupo");

                entity.Property(e => e.IdInstitucion).HasColumnName("id_Institucion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(800)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .HasColumnName("titulo");

                entity.Property(e => e.Trato)
                    .HasMaxLength(100)
                    .HasColumnName("trato");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK_Personas_Grupos");

                entity.HasOne(d => d.IdInstitucionNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdInstitucion)
                    .HasConstraintName("FK_Personas_Instituciones");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("usuarios");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("usuariosHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdUsuario).HasColumnName("id_Usuario");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Creacion");

                entity.Property(e => e.IdUsuarioTipo).HasColumnName("id_UsuarioTipo");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .HasColumnName("nombres");

                entity.Property(e => e.UltimaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("ultima_Modificacion");

                entity.HasOne(d => d.IdUsuarioTipoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdUsuarioTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuarios_usuarios_tipos");
            });

            modelBuilder.Entity<UsuariosTipo>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioTipo);

                entity.ToTable("usuarios_tipos");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("usuarios_tiposHistory", "dbo");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

                entity.Property(e => e.IdUsuarioTipo).HasColumnName("id_UsuarioTipo");

                entity.Property(e => e.UsuarioTipo).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
