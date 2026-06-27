using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LegacyBarber.App.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateTable(
                name: "estados_cita",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados_cita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    EsSistema = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "archivos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    nombre_original = table.Column<string>(type: "text", nullable: false),
                    nombre_almacenado = table.Column<string>(type: "text", nullable: false),
                    Ruta = table.Column<string>(type: "text", nullable: false),
                    tipo_mime = table.Column<string>(type: "text", nullable: true),
                    tamano_bytes = table.Column<long>(type: "bigint", nullable: true),
                    entidad_tipo = table.Column<string>(type: "text", nullable: true),
                    entidad_id = table.Column<long>(type: "bigint", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "barberias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Nit = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    HorarioAtencion = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    logo_id = table.Column<long>(type: "bigint", nullable: true),
                    Configuracion = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barberias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_barberias_archivos_logo_id",
                        column: x => x.logo_id,
                        principalTable: "archivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "categorias_servicios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Orden = table.Column<short>(type: "smallint", nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias_servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categorias_servicios_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "configuraciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    Clave = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<string>(type: "jsonb", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_configuraciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_configuraciones_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "metodos_pago",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    Configuracion = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metodos_pago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_metodos_pago_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "citext", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    nombre_completo = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    foto_id = table.Column<long>(type: "bigint", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    email_verificado = table.Column<bool>(type: "boolean", nullable: false),
                    ultimo_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarios_archivos_foto_id",
                        column: x => x.foto_id,
                        principalTable: "archivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_usuarios_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "servicios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    categoria_id = table.Column<long>(type: "bigint", nullable: true),
                    imagen_id = table.Column<long>(type: "bigint", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    duracion_minutos = table.Column<short>(type: "smallint", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_servicios_archivos_imagen_id",
                        column: x => x.imagen_id,
                        principalTable: "archivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_servicios_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_servicios_categorias_servicios_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categorias_servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "auditoria",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    Tabla = table.Column<string>(type: "text", nullable: false),
                    registro_id = table.Column<long>(type: "bigint", nullable: false),
                    Accion = table.Column<string>(type: "text", nullable: false),
                    datos_anteriores = table.Column<string>(type: "jsonb", nullable: true),
                    datos_nuevos = table.Column<string>(type: "jsonb", nullable: true),
                    usuario_id = table.Column<long>(type: "bigint", nullable: true),
                    ip_address = table.Column<string>(type: "text", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_auditoria_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_auditoria_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "barberos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    Biografia = table.Column<string>(type: "text", nullable: true),
                    Especialidades = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "[]"),
                    anios_experiencia = table.Column<short>(type: "smallint", nullable: true),
                    comision_porcentaje = table.Column<decimal>(type: "numeric", nullable: false),
                    calificacion_promedio = table.Column<decimal>(type: "numeric", nullable: false),
                    total_resenas = table.Column<int>(type: "integer", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barberos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_barberos_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_barberos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notas = table.Column<string>(type: "text", nullable: true),
                    Preferencias = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clientes_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clientes_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notificaciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    Canal = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Asunto = table.Column<string>(type: "text", nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    Datos = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    enviada_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    leida_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notificaciones_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notificaciones_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tokens_refresco",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens_refresco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tokens_refresco_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    RolId = table.Column<long>(type: "bigint", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuario_roles_roles_RolId",
                        column: x => x.RolId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_roles_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "barbero_servicios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    servicio_id = table.Column<long>(type: "bigint", nullable: false),
                    precio_personalizado = table.Column<decimal>(type: "numeric", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barbero_servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_barbero_servicios_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_barbero_servicios_servicios_servicio_id",
                        column: x => x.servicio_id,
                        principalTable: "servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "excepciones_horarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "interval", nullable: true),
                    hora_fin = table.Column<TimeSpan>(type: "interval", nullable: true),
                    esta_disponible = table.Column<bool>(type: "boolean", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_excepciones_horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_excepciones_horarios_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "horarios_barberos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    dia_semana = table.Column<short>(type: "smallint", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    hora_fin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios_barberos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_horarios_barberos_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "citas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    cliente_id = table.Column<long>(type: "bigint", nullable: false),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    estado_cita_id = table.Column<long>(type: "bigint", nullable: false),
                    cita_original_id = table.Column<long>(type: "bigint", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    hora_fin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    duracion_total_minutos = table.Column<short>(type: "smallint", nullable: false),
                    precio_total = table.Column<decimal>(type: "numeric", nullable: false),
                    Notas = table.Column<string>(type: "text", nullable: true),
                    Origen = table.Column<string>(type: "text", nullable: false),
                    motivo_cancelacion = table.Column<string>(type: "text", nullable: true),
                    cancelado_por = table.Column<long>(type: "bigint", nullable: true),
                    Pagado = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citas", x => x.Id);
                    table.CheckConstraint("chk_cita_horas", "hora_inicio < hora_fin");
                    table.ForeignKey(
                        name: "FK_citas_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_citas_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_citas_citas_cita_original_id",
                        column: x => x.cita_original_id,
                        principalTable: "citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_citas_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_citas_estados_cita_estado_cita_id",
                        column: x => x.estado_cita_id,
                        principalTable: "estados_cita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cita_servicios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cita_id = table.Column<long>(type: "bigint", nullable: false),
                    servicio_id = table.Column<long>(type: "bigint", nullable: false),
                    precio_aplicado = table.Column<decimal>(type: "numeric", nullable: false),
                    duracion_minutos = table.Column<short>(type: "smallint", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cita_servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cita_servicios_citas_cita_id",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cita_servicios_servicios_servicio_id",
                        column: x => x.servicio_id,
                        principalTable: "servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    cita_id = table.Column<long>(type: "bigint", nullable: false),
                    cliente_id = table.Column<long>(type: "bigint", nullable: false),
                    metodo_pago_id = table.Column<long>(type: "bigint", nullable: false),
                    registrado_por = table.Column<long>(type: "bigint", nullable: true),
                    Monto = table.Column<decimal>(type: "numeric", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    referencia_externa = table.Column<string>(type: "text", nullable: true),
                    transaction_id_externo = table.Column<string>(type: "text", nullable: true),
                    datos_respuesta = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    pagado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reembolsado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BarberoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pagos_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pagos_barberos_BarberoId",
                        column: x => x.BarberoId,
                        principalTable: "barberos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_pagos_citas_cita_id",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pagos_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pagos_metodos_pago_metodo_pago_id",
                        column: x => x.metodo_pago_id,
                        principalTable: "metodos_pago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pagos_usuarios_registrado_por",
                        column: x => x.registrado_por,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "resenas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cita_id = table.Column<long>(type: "bigint", nullable: false),
                    cliente_id = table.Column<long>(type: "bigint", nullable: false),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    Calificacion = table.Column<short>(type: "smallint", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    Visible = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resenas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resenas_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_resenas_citas_cita_id",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_resenas_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "estados_cita",
                columns: new[] { "Id", "Color", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1L, "#D3B40E", "Cita creada, esperando pago o confirmación", "pendiente" },
                    { 2L, "#28c840", "Cita confirmada", "confirmada" },
                    { 3L, "#D3B40E", "Cliente está siendo atendido", "en_curso" },
                    { 4L, "#28c840", "Servicio finalizado", "completada" },
                    { 5L, "#ff5a5a", "Cancelada por el cliente", "cancelada_por_cliente" },
                    { 6L, "#ff5a5a", "Cancelada por el barbero o admin", "cancelada_por_barbero" },
                    { 7L, "#ff5a5a", "Cancelada por no pago u otra regla", "cancelada_por_sistema" },
                    { 8L, "#ff5a5a", "Cliente no asistió", "no_show" },
                    { 9L, "#D3B40E", "Cita reprogramada, reemplazada por otra", "reprogramada" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "creado_en", "Descripcion", "EsSistema", "Nombre" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 6, 27, 16, 34, 11, 206, DateTimeKind.Utc).AddTicks(5644), "Superadministrador del sistema Legacy Barber", true, "superadmin" },
                    { 2L, new DateTime(2026, 6, 27, 16, 34, 11, 206, DateTimeKind.Utc).AddTicks(5647), "Administrador de la barbería", true, "admin" },
                    { 3L, new DateTime(2026, 6, 27, 16, 34, 11, 206, DateTimeKind.Utc).AddTicks(5648), "Barbero que atiende citas", true, "barbero" },
                    { 4L, new DateTime(2026, 6, 27, 16, 34, 11, 206, DateTimeKind.Utc).AddTicks(5648), "Cliente que agenda citas", true, "cliente" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_archivos_barberia_id",
                table: "archivos",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "IX_auditoria_barberia_id",
                table: "auditoria",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "IX_auditoria_usuario_id",
                table: "auditoria",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_barberias_Email",
                table: "barberias",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_barberias_logo_id",
                table: "barberias",
                column: "logo_id");

            migrationBuilder.CreateIndex(
                name: "IX_barbero_servicios_barbero_id_servicio_id",
                table: "barbero_servicios",
                columns: new[] { "barbero_id", "servicio_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_barbero_servicios_servicio_id",
                table: "barbero_servicios",
                column: "servicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_barberos_barberia_id",
                table: "barberos",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "IX_barberos_usuario_id",
                table: "barberos",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categorias_servicios_barberia_id_Nombre",
                table: "categorias_servicios",
                columns: new[] { "barberia_id", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cita_servicios_cita_id_servicio_id",
                table: "cita_servicios",
                columns: new[] { "cita_id", "servicio_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cita_servicios_servicio_id",
                table: "cita_servicios",
                column: "servicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_citas_barberia_id",
                table: "citas",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "IX_citas_barbero_id_Fecha_hora_inicio",
                table: "citas",
                columns: new[] { "barbero_id", "Fecha", "hora_inicio" });

            migrationBuilder.CreateIndex(
                name: "IX_citas_cita_original_id",
                table: "citas",
                column: "cita_original_id");

            migrationBuilder.CreateIndex(
                name: "IX_citas_cliente_id_Fecha",
                table: "citas",
                columns: new[] { "cliente_id", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_citas_estado_cita_id",
                table: "citas",
                column: "estado_cita_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_barberia_id_usuario_id",
                table: "clientes",
                columns: new[] { "barberia_id", "usuario_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clientes_usuario_id",
                table: "clientes",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_configuraciones_barberia_id_Clave",
                table: "configuraciones",
                columns: new[] { "barberia_id", "Clave" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_estados_cita_Nombre",
                table: "estados_cita",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_excepciones_horarios_barbero_id_Fecha",
                table: "excepciones_horarios",
                columns: new[] { "barbero_id", "Fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_horarios_barberos_barbero_id_dia_semana_hora_inicio",
                table: "horarios_barberos",
                columns: new[] { "barbero_id", "dia_semana", "hora_inicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_metodos_pago_barberia_id_Codigo",
                table: "metodos_pago",
                columns: new[] { "barberia_id", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_barberia_id",
                table: "notificaciones",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_usuario_id_estado",
                table: "notificaciones",
                columns: new[] { "usuario_id", "estado" },
                filter: "\"estado\" = 'pendiente'");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_barberia_id",
                table: "pagos",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_BarberoId",
                table: "pagos",
                column: "BarberoId");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_cita_id",
                table: "pagos",
                column: "cita_id");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_cliente_id",
                table: "pagos",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_metodo_pago_id",
                table: "pagos",
                column: "metodo_pago_id");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_registrado_por",
                table: "pagos",
                column: "registrado_por");

            migrationBuilder.CreateIndex(
                name: "IX_resenas_barbero_id",
                table: "resenas",
                column: "barbero_id");

            migrationBuilder.CreateIndex(
                name: "IX_resenas_cita_id",
                table: "resenas",
                column: "cita_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_resenas_cliente_id",
                table: "resenas",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_Nombre",
                table: "roles",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_servicios_barberia_id_Nombre",
                table: "servicios",
                columns: new[] { "barberia_id", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_servicios_categoria_id",
                table: "servicios",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_servicios_imagen_id",
                table: "servicios",
                column: "imagen_id");

            migrationBuilder.CreateIndex(
                name: "IX_tokens_refresco_Token",
                table: "tokens_refresco",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tokens_refresco_usuario_id",
                table: "tokens_refresco",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_roles_RolId",
                table: "usuario_roles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_roles_UsuarioId_RolId",
                table: "usuario_roles",
                columns: new[] { "UsuarioId", "RolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_barberia_id_Email",
                table: "usuarios",
                columns: new[] { "barberia_id", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_foto_id",
                table: "usuarios",
                column: "foto_id");

            migrationBuilder.AddForeignKey(
                name: "FK_archivos_barberias_barberia_id",
                table: "archivos",
                column: "barberia_id",
                principalTable: "barberias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_archivos_barberias_barberia_id",
                table: "archivos");

            migrationBuilder.DropTable(
                name: "auditoria");

            migrationBuilder.DropTable(
                name: "barbero_servicios");

            migrationBuilder.DropTable(
                name: "cita_servicios");

            migrationBuilder.DropTable(
                name: "configuraciones");

            migrationBuilder.DropTable(
                name: "excepciones_horarios");

            migrationBuilder.DropTable(
                name: "horarios_barberos");

            migrationBuilder.DropTable(
                name: "notificaciones");

            migrationBuilder.DropTable(
                name: "pagos");

            migrationBuilder.DropTable(
                name: "resenas");

            migrationBuilder.DropTable(
                name: "tokens_refresco");

            migrationBuilder.DropTable(
                name: "usuario_roles");

            migrationBuilder.DropTable(
                name: "servicios");

            migrationBuilder.DropTable(
                name: "metodos_pago");

            migrationBuilder.DropTable(
                name: "citas");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "categorias_servicios");

            migrationBuilder.DropTable(
                name: "barberos");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "estados_cita");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "barberias");

            migrationBuilder.DropTable(
                name: "archivos");
        }
    }
}
