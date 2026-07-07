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
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estados_cita", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    es_sistema = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "archivos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    nombre_original = table.Column<string>(type: "text", nullable: false),
                    nombre_almacenado = table.Column<string>(type: "text", nullable: false),
                    ruta = table.Column<string>(type: "text", nullable: false),
                    tipo_mime = table.Column<string>(type: "text", nullable: true),
                    tamano_bytes = table.Column<long>(type: "bigint", nullable: true),
                    entidad_tipo = table.Column<string>(type: "text", nullable: true),
                    entidad_id = table.Column<long>(type: "bigint", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_archivos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "barberias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    nit = table.Column<string>(type: "text", nullable: true),
                    direccion = table.Column<string>(type: "text", nullable: true),
                    telefono = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    horario_atencion = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    logo_id = table.Column<long>(type: "bigint", nullable: true),
                    configuracion = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    activa = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_barberias", x => x.id);
                    table.ForeignKey(
                        name: "fk_barberias_archivos_logo_id",
                        column: x => x.logo_id,
                        principalTable: "archivos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "categorias_servicios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    orden = table.Column<short>(type: "smallint", nullable: false),
                    activa = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorias_servicios", x => x.id);
                    table.ForeignKey(
                        name: "fk_categorias_servicios_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "configuraciones",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    clave = table.Column<string>(type: "text", nullable: false),
                    valor = table.Column<string>(type: "jsonb", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_configuraciones", x => x.id);
                    table.ForeignKey(
                        name: "fk_configuraciones_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "metodos_pago",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    configuracion = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_metodos_pago", x => x.id);
                    table.ForeignKey(
                        name: "fk_metodos_pago_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: true),
                    email = table.Column<string>(type: "citext", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    nombre_completo = table.Column<string>(type: "text", nullable: false),
                    telefono = table.Column<string>(type: "text", nullable: true),
                    foto_id = table.Column<long>(type: "bigint", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    email_verificado = table.Column<bool>(type: "boolean", nullable: false),
                    ultimo_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuarios_archivos_foto_id",
                        column: x => x.foto_id,
                        principalTable: "archivos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_usuarios_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "servicios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    categoria_id = table.Column<long>(type: "bigint", nullable: true),
                    imagen_id = table.Column<long>(type: "bigint", nullable: true),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    duracion_minutos = table.Column<short>(type: "smallint", nullable: false),
                    precio = table.Column<decimal>(type: "numeric", nullable: false),
                    color = table.Column<string>(type: "text", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicios", x => x.id);
                    table.ForeignKey(
                        name: "fk_servicios_archivos_imagen_id",
                        column: x => x.imagen_id,
                        principalTable: "archivos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_servicios_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_servicios_categorias_servicios_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categorias_servicios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "auditorias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    tabla = table.Column<string>(type: "text", nullable: false),
                    registro_id = table.Column<long>(type: "bigint", nullable: false),
                    accion = table.Column<string>(type: "text", nullable: false),
                    datos_anteriores = table.Column<string>(type: "jsonb", nullable: true),
                    datos_nuevos = table.Column<string>(type: "jsonb", nullable: true),
                    usuario_id = table.Column<long>(type: "bigint", nullable: true),
                    ip_address = table.Column<string>(type: "text", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auditorias", x => x.id);
                    table.ForeignKey(
                        name: "fk_auditorias_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_auditorias_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "barberos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    biografia = table.Column<string>(type: "text", nullable: true),
                    especialidades = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "[]"),
                    anios_experiencia = table.Column<short>(type: "smallint", nullable: true),
                    comision_porcentaje = table.Column<decimal>(type: "numeric", nullable: false),
                    calificacion_promedio = table.Column<decimal>(type: "numeric", nullable: false),
                    total_resenas = table.Column<int>(type: "integer", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_barberos", x => x.id);
                    table.ForeignKey(
                        name: "fk_barberos_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_barberos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    notas = table.Column<string>(type: "text", nullable: true),
                    preferencias = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clientes", x => x.id);
                    table.ForeignKey(
                        name: "fk_clientes_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notificaciones",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    canal = table.Column<string>(type: "text", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    asunto = table.Column<string>(type: "text", nullable: false),
                    contenido = table.Column<string>(type: "text", nullable: false),
                    datos = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    enviada_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    leida_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notificaciones", x => x.id);
                    table.ForeignKey(
                        name: "fk_notificaciones_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notificaciones_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tokens_refresco",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "text", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tokens_refresco", x => x.id);
                    table.ForeignKey(
                        name: "fk_tokens_refresco_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    rol_id = table.Column<long>(type: "bigint", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuario_roles_roles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario_roles_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "barbero_servicios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    servicio_id = table.Column<long>(type: "bigint", nullable: false),
                    precio_personalizado = table.Column<decimal>(type: "numeric", nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_barbero_servicios", x => x.id);
                    table.ForeignKey(
                        name: "fk_barbero_servicios_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_barbero_servicios_servicios_servicio_id",
                        column: x => x.servicio_id,
                        principalTable: "servicios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "excepciones_horarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "interval", nullable: true),
                    hora_fin = table.Column<TimeSpan>(type: "interval", nullable: true),
                    esta_disponible = table.Column<bool>(type: "boolean", nullable: false),
                    motivo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_excepciones_horarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_excepciones_horarios_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "horarios_barberos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    dia_semana = table.Column<short>(type: "smallint", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    hora_fin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_horarios_barberos", x => x.id);
                    table.ForeignKey(
                        name: "fk_horarios_barberos_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "citas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    cliente_id = table.Column<long>(type: "bigint", nullable: false),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    estado_cita_id = table.Column<long>(type: "bigint", nullable: false),
                    cita_original_id = table.Column<long>(type: "bigint", nullable: true),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hora_inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    hora_fin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    duracion_total_minutos = table.Column<short>(type: "smallint", nullable: false),
                    precio_total = table.Column<decimal>(type: "numeric", nullable: false),
                    notas = table.Column<string>(type: "text", nullable: true),
                    origen = table.Column<string>(type: "text", nullable: false),
                    motivo_cancelacion = table.Column<string>(type: "text", nullable: true),
                    cancelado_por = table.Column<long>(type: "bigint", nullable: true),
                    pagado = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_citas", x => x.id);
                    table.CheckConstraint("chk_cita_horas", "hora_inicio < hora_fin");
                    table.ForeignKey(
                        name: "fk_citas_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_citas_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_citas_citas_cita_original_id",
                        column: x => x.cita_original_id,
                        principalTable: "citas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_citas_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_citas_estados_cita_estado_cita_id",
                        column: x => x.estado_cita_id,
                        principalTable: "estados_cita",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cita_servicios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cita_id = table.Column<long>(type: "bigint", nullable: false),
                    servicio_id = table.Column<long>(type: "bigint", nullable: false),
                    precio_aplicado = table.Column<decimal>(type: "numeric", nullable: false),
                    duracion_minutos = table.Column<short>(type: "smallint", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cita_servicios", x => x.id);
                    table.ForeignKey(
                        name: "fk_cita_servicios_citas_cita_id",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cita_servicios_servicios_servicio_id",
                        column: x => x.servicio_id,
                        principalTable: "servicios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barberia_id = table.Column<long>(type: "bigint", nullable: false),
                    cita_id = table.Column<long>(type: "bigint", nullable: false),
                    cliente_id = table.Column<long>(type: "bigint", nullable: false),
                    metodo_pago_id = table.Column<long>(type: "bigint", nullable: false),
                    registrado_por = table.Column<long>(type: "bigint", nullable: true),
                    monto = table.Column<decimal>(type: "numeric", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false),
                    referencia_externa = table.Column<string>(type: "text", nullable: true),
                    transaction_id_externo = table.Column<string>(type: "text", nullable: true),
                    datos_respuesta = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    pagado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reembolsado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pagos", x => x.id);
                    table.ForeignKey(
                        name: "fk_pagos_barberias_barberia_id",
                        column: x => x.barberia_id,
                        principalTable: "barberias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pagos_citas_cita_id",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pagos_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pagos_metodos_pago_metodo_pago_id",
                        column: x => x.metodo_pago_id,
                        principalTable: "metodos_pago",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pagos_usuarios_registrado_por",
                        column: x => x.registrado_por,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "resenas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cita_id = table.Column<long>(type: "bigint", nullable: false),
                    cliente_id = table.Column<long>(type: "bigint", nullable: false),
                    barbero_id = table.Column<long>(type: "bigint", nullable: false),
                    calificacion = table.Column<short>(type: "smallint", nullable: false),
                    comentario = table.Column<string>(type: "text", nullable: true),
                    visible = table.Column<bool>(type: "boolean", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resenas", x => x.id);
                    table.ForeignKey(
                        name: "fk_resenas_barberos_barbero_id",
                        column: x => x.barbero_id,
                        principalTable: "barberos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_resenas_citas_cita_id",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_resenas_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "estados_cita",
                columns: new[] { "id", "color", "descripcion", "nombre" },
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
                columns: new[] { "id", "creado_en", "descripcion", "es_sistema", "nombre" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 6, 27, 18, 17, 10, 148, DateTimeKind.Utc).AddTicks(1019), "Superadministrador del sistema Legacy Barber", true, "superadmin" },
                    { 2L, new DateTime(2026, 6, 27, 18, 17, 10, 148, DateTimeKind.Utc).AddTicks(1024), "Administrador de la barbería", true, "admin" },
                    { 3L, new DateTime(2026, 6, 27, 18, 17, 10, 148, DateTimeKind.Utc).AddTicks(1025), "Barbero que atiende citas", true, "barbero" },
                    { 4L, new DateTime(2026, 6, 27, 18, 17, 10, 148, DateTimeKind.Utc).AddTicks(1026), "Cliente que agenda citas", true, "cliente" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_archivos_barberia_id",
                table: "archivos",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "ix_auditorias_barberia_id",
                table: "auditorias",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "ix_auditorias_usuario_id",
                table: "auditorias",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_barberias_email",
                table: "barberias",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_barberias_logo_id",
                table: "barberias",
                column: "logo_id");

            migrationBuilder.CreateIndex(
                name: "ix_barbero_servicios_barbero_id_servicio_id",
                table: "barbero_servicios",
                columns: new[] { "barbero_id", "servicio_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_barbero_servicios_servicio_id",
                table: "barbero_servicios",
                column: "servicio_id");

            migrationBuilder.CreateIndex(
                name: "ix_barberos_barberia_id",
                table: "barberos",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "ix_barberos_usuario_id",
                table: "barberos",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_categorias_servicios_barberia_id_nombre",
                table: "categorias_servicios",
                columns: new[] { "barberia_id", "nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cita_servicios_cita_id_servicio_id",
                table: "cita_servicios",
                columns: new[] { "cita_id", "servicio_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cita_servicios_servicio_id",
                table: "cita_servicios",
                column: "servicio_id");

            migrationBuilder.CreateIndex(
                name: "ix_citas_barberia_id",
                table: "citas",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "ix_citas_barbero_id_fecha_hora_inicio",
                table: "citas",
                columns: new[] { "barbero_id", "fecha", "hora_inicio" });

            migrationBuilder.CreateIndex(
                name: "ix_citas_cita_original_id",
                table: "citas",
                column: "cita_original_id");

            migrationBuilder.CreateIndex(
                name: "ix_citas_cliente_id_fecha",
                table: "citas",
                columns: new[] { "cliente_id", "fecha" });

            migrationBuilder.CreateIndex(
                name: "ix_citas_estado_cita_id",
                table: "citas",
                column: "estado_cita_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_barberia_id_usuario_id",
                table: "clientes",
                columns: new[] { "barberia_id", "usuario_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clientes_usuario_id",
                table: "clientes",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_configuraciones_barberia_id_clave",
                table: "configuraciones",
                columns: new[] { "barberia_id", "clave" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_estados_cita_nombre",
                table: "estados_cita",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_excepciones_horarios_barbero_id_fecha",
                table: "excepciones_horarios",
                columns: new[] { "barbero_id", "fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_horarios_barberos_barbero_id_dia_semana_hora_inicio",
                table: "horarios_barberos",
                columns: new[] { "barbero_id", "dia_semana", "hora_inicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_metodos_pago_barberia_id_codigo",
                table: "metodos_pago",
                columns: new[] { "barberia_id", "codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_notificaciones_barberia_id",
                table: "notificaciones",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "ix_notificaciones_usuario_id_estado",
                table: "notificaciones",
                columns: new[] { "usuario_id", "estado" },
                filter: "\"estado\" = 'pendiente'");

            migrationBuilder.CreateIndex(
                name: "ix_pagos_barberia_id",
                table: "pagos",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "ix_pagos_cita_id",
                table: "pagos",
                column: "cita_id");

            migrationBuilder.CreateIndex(
                name: "ix_pagos_cliente_id",
                table: "pagos",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "ix_pagos_metodo_pago_id",
                table: "pagos",
                column: "metodo_pago_id");

            migrationBuilder.CreateIndex(
                name: "ix_pagos_registrado_por",
                table: "pagos",
                column: "registrado_por");

            migrationBuilder.CreateIndex(
                name: "ix_resenas_barbero_id",
                table: "resenas",
                column: "barbero_id");

            migrationBuilder.CreateIndex(
                name: "ix_resenas_cita_id",
                table: "resenas",
                column: "cita_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_resenas_cliente_id",
                table: "resenas",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_nombre",
                table: "roles",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_servicios_barberia_id_nombre",
                table: "servicios",
                columns: new[] { "barberia_id", "nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_servicios_categoria_id",
                table: "servicios",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_servicios_imagen_id",
                table: "servicios",
                column: "imagen_id");

            migrationBuilder.CreateIndex(
                name: "ix_tokens_refresco_token",
                table: "tokens_refresco",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tokens_refresco_usuario_id",
                table: "tokens_refresco",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_roles_rol_id",
                table: "usuario_roles",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_roles_usuario_id_rol_id",
                table: "usuario_roles",
                columns: new[] { "usuario_id", "rol_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_barberia_id",
                table: "usuarios",
                column: "barberia_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_foto_id",
                table: "usuarios",
                column: "foto_id");

            migrationBuilder.AddForeignKey(
                name: "fk_archivos_barberias_barberia_id",
                table: "archivos",
                column: "barberia_id",
                principalTable: "barberias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_archivos_barberias_barberia_id",
                table: "archivos");

            migrationBuilder.DropTable(
                name: "auditorias");

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
