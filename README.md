# LegacyBarber.Api

Backend .NET 8 para **Legacy Barber**, un sistema de agendamiento de citas para barberías.

## Propósito

SaaS multi-barbería que permite:
- Gestión de barberías, servicios, barberos y clientes.
- Agendamiento de citas con control de disponibilidad.
- Pagos online (MercadoPago) y en efectivo.
- Notificaciones por email.
- Roles simples: `superadmin`, `admin`, `barbero`, `cliente`.

## Stack tecnológico

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core 8**
- **PostgreSQL** con Npgsql
- **JWT** para autenticación
- **FluentValidation** para validación de entrada
- **Serilog** para logging
- **Clean Architecture** (Domain, Core, DataAccess, Api)

## Arquitectura

```
App.Domain      -> Entidades, value objects, excepciones de dominio. Sin dependencias.
App.Core        -> Interfaces, servicios de aplicación, modelos, validadores. Depende de App.Domain.
App.DataAccess  -> DbContext, repositorios, migraciones. Depende de App.Core y App.Domain.
App.Api         -> Controllers, middleware, configuración DI, seguridad e infraestructura web. Depende de App.Core y App.DataAccess.
App.Util        -> Utilidades transversales. Depende de App.Domain y App.Core.
```

**Reglas importantes:**
- `App.Domain` no depende de nadie.
- `App.Core` no depende de `App.DataAccess`, `App.Api` ni `App.Util`.
- `App.DataAccess` no depende de `App.Util`.
- `App.Util` depende de `App.Domain` y `App.Core` para helpers que operan sobre el dominio.
- Se usa un **repositorio genérico base** (`IRepository<TEntity, TId>` / `BaseRepository<TEntity, TId>`) para CRUD común, más **repositorios específicos por agregado** para consultas de dominio.
- Los repositorios **no llaman `SaveChanges`**. La capa de aplicación coordina el commit mediante `IUnitOfWork`.

## Decisiones de diseño ya tomadas

### Autenticación y usuarios
- **Email único global**: un email solo puede pertenecer a un usuario en todo el sistema.
- El **superadmin** tiene `BarberiaId = null` y es global.
- Los roles se almacenan en la tabla `roles` y se relacionan con `usuarios` mediante `usuario_roles`.
- Autenticación con JWT + refresh tokens (rotación de tokens).

### Validación y seguridad
- Validación automática de entrada con **FluentValidation** mediante `ValidationFilter` global.
- Validación de `JwtSettings` al arrancar (clave, issuer, audience).
- Repositorios de lectura usan `AsNoTracking`; `GetByIdAsync` devuelve la entidad trackeada para permitir edición.

### Entidades clave
- `Barberia`: cada barbería es un tenant.
- `Usuario`: usuario del sistema (superadmin, admin, barbero, cliente potencial).
- `Cliente` / `Barbero`: perfiles ligados a un `Usuario` y a una `Barberia`.
- `Servicio`: servicios ofrecidos por una barbería.
- `Cita`: agendamiento con barbero, cliente, fecha, hora, servicios y estado.
- `Pago`: pagos asociados a una cita. No tiene FK directa a barbero; se accede vía `Cita.BarberoId`.
- `EstadoCita` y `Rol`: catálogos semilla con IDs fijos.

### Base de datos
- **PostgreSQL** en localhost para desarrollo.
- IDs autoincrementales `BIGSERIAL`.
- Columnas en **snake_case** mediante `EFCore.NamingConventions`.
- Uso de `jsonb` para campos JSON y `citext` para emails.
- Seed inicial de `roles` y `estados_cita` mediante `HasData`.

### Pagos
- Métodos soportados: MercadoPago y efectivo.
- Sin cupones, promociones ni programa de fidelización.

### Notificaciones
- Solo email por ahora. WhatsApp es posible futuro.

### Imágenes
- Subida de archivos. Las imágenes se almacenan como entradas en `archivos`.

## Configuración de secretos

Nunca versionar secretos. Usar **User Secrets** en desarrollo o variables de entorno en producción.

### User Secrets (desarrollo)

```bash
cd App.Api
dotnet user-secrets init   # ya está inicializado
dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Database=legacy_barber;Username=postgres;Password=TU_PASSWORD"
dotnet user-secrets set "JwtSettings:SecretKey" "TU_CLAVE_DE_AL_MENOS_32_CARACTERES"
dotnet user-secrets set "SuperAdmin:Password" "TU_PASSWORD_SEGURA"
```

### Variables de entorno (producción)

```bash
ConnectionStrings__Default="Host=...;Database=...;Username=...;Password=..."
JwtSettings__SecretKey="..."
SuperAdmin__Password="..."
```

## Cómo ejecutar

### Requisitos
- .NET 8 SDK
- PostgreSQL 14+

### Pasos

1. Configurar secretos (ver sección anterior).
2. Asegurarse de que PostgreSQL esté corriendo.
3. La base de datos se crea/aplica automáticamente al arrancar (`DatabaseSeeder` ejecuta `MigrateAsync`).

```bash
cd LegacyBarber.Api
dotnet build LegacyBarber.Api.sln
dotnet run --project App.Api\App.Api.csproj --urls "http://localhost:5000"
```

4. Abrir Swagger: `http://localhost:5000/swagger`

### Credenciales iniciales

- **Email:** `admin@legacybarber.app`
- **Contraseña:** valor configurado en `SuperAdmin:Password`.

## Convenciones de código

- Español para nombres de dominio (`Cita`, `Barbero`, `Pago`, `Resena`).
- Inglés para conceptos técnicos (`DbContext`, `Repository`, `Service`).
- Propiedades de entidades en español con snake_case en base de datos.
- Servicios y repositorios asíncronos con `CancellationToken`.
- Validadores FluentValidation en `App.Core.Validators`.
- Modelos de entrada en `App.Core.Model` (temporalmente; a futuro separar en contratos de API).

## Estructura de carpetas

```
LegacyBarber.Api/
├── App.Api/
│   ├── Controllers/         -> API controllers
│   ├── Data/                -> DatabaseSeeder
│   ├── Extensions/          -> Configuración de DI
│   ├── Filters/             -> ValidationFilter
│   ├── Middleware/          -> ExceptionMiddleware
│   ├── Properties/
│   ├── Security/            -> AspNetPasswordHasher, JwtTokenService
│   ├── Utils/               -> CurrentUserService
│   └── appsettings.json     -> Sin secretos
├── App.Core/
│   ├── Interfaces/          -> Contratos de servicios y repositorios
│   ├── Model/               -> Modelos de entrada/salida
│   ├── Security/            -> JwtSettings
│   ├── Services/            -> Servicios de aplicación por dominio (BarberiaService, CitaService, etc.)
│   └── Validators/          -> FluentValidation validators
├── App.DataAccess/
│   ├── DbContext/           -> AppDbContext
│   ├── Migrations/          -> Migraciones EF Core
│   └── Repos/               -> Implementaciones de repositorios
├── App.Domain/
│   ├── Entities/            -> Entidades de dominio
│   ├── Exceptions/          -> Excepciones de dominio
│   └── ValueObjects/        -> Value objects (HorarioAtencion, Dinero, etc.)
└── App.Util/                -> Utilidades puras (reservado)
```

## Notas para agentes de IA

- **No preguntar por stack ni arquitectura**: está definido arriba.
- **No preguntar por decisiones de diseño**: email global, superadmin sin barbería, roles simples, pagos MercadoPago + efectivo, notificaciones email.
- **No hardcodear secretos**: siempre usar `IConfiguration` y User Secrets/variables de entorno.
- **Mantener dependencias de proyectos respetando la regla de Clean Architecture**.
- **Regenerar migraciones si se modifican entidades**: eliminar migración anterior, dropear BD local, crear nueva `InitialCreate`, aplicar.
- **Usar snake_case para base de datos** mediante `UseSnakeCaseNamingConvention()`.
- **Agregar validadores FluentValidation** en `App.Core.Validators` cuando se creen nuevos modelos de entrada.

## Pendientes conocidos

- [ ] Mover infraestructura de seguridad (`JwtTokenService`, `AspNetPasswordHasher`) fuera de `App.Api`.
- [ ] Implementar paginación y filtrado por `BarberiaId` en listados.
- [ ] Agregar transacciones explícitas a `IUnitOfWork` si se requiere atomicidad multi-repositorio.
- [ ] Configurar Swagger con esquema Bearer.
- [ ] Agregar rate limiting y CORS.
- [ ] Implementar constraint `EXCLUDE` de PostgreSQL para evitar citas solapadas.
- [ ] Crear proyectos de pruebas unitarias e integración.
