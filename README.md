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

## Decisiones de arquitectura actualizadas

Este apartado consolida las decisiones tomadas para el modelo de identidad, registro de usuarios, registro de barberías y flujo de negocio. Está vigente a partir de julio de 2026.

### Modelo de identidad

- **Email único global**: una misma persona tiene una sola cuenta en Legacy Barber, sin importar a cuántas barberías esté asociada.
- **Relación usuario-barbería**:
  - Un **cliente** puede pertenecer a varias barberías (relación N:M a través de la tabla `Cliente`).
  - Un **barbero** pertenece a una sola barbería (a través de la tabla `Barbero`).
  - Un **admin** pertenece a una sola barbería (a través de `Usuario.BarberiaId` o la entidad `Barberia`).
- **Roles globales**: `superadmin`, `admin`, `barbero`, `cliente`. Un usuario puede tener múltiples roles (por ejemplo, admin y barbero al mismo tiempo).

### Flujo de registro de clientes

1. El visitante llega a la landing general (`legacybarber.app`) y se registra como usuario global.
2. Campos del registro: nombre completo, email, teléfono y contraseña.
3. No hay login automático; tras registrarse se muestra un mensaje de éxito y debe iniciar sesión manualmente.
4. Al iniciar sesión, si aún no está asociado a ninguna barbería, ve el marketplace con las barberías disponibles.
5. Selecciona una barbería y se crea el vínculo `Cliente`.
6. A partir de ahí accede al dashboard de cliente, servicios, barberos y citas.

> En el diseño final el registro requerirá verificación de email antes de poder iniciar sesión. Para la primera fase esa verificación no se implementa y `EmailVerificado` se marca como `true` temporalmente.

### Flujo de registro de barberías (self-service)

1. El dueño se registra como usuario global.
2. Crea su barbería mediante un asistente: nombre, slug, dirección, ciudad, teléfono, email de contacto y logo (opcional).
3. La barbería queda en estado `EnConfiguracion`.
4. El dueño puede configurar todo: servicios, barberos, horarios de atención, métodos de pago, etc.
5. No aparece en el marketplace público ni recibe citas reales mientras esté en configuración.
6. Para activarla debe pagar la suscripción mensual adelantada.
7. Tras el pago, la barbería pasa a estado `Activa` y puede operar normalmente.

El superadmin no crea barberías de forma rutinaria, pero conserva esa función de respaldo para casos especiales o demos.

### Estados de barbería

| Estado | Descripción |
|--------|-------------|
| `EnConfiguracion` | Recién creada. Solo permite configuración interna. No recibe citas ni aparece públicamente. |
| `Activa` | Pagó la suscripción. Aparece en el marketplace y recibe citas. |
| `Suspendida` | El superadmin la suspendió (incumplimiento, soporte, etc.). |
| `Cancelada` | El dueño dio de baja la barbería o se eliminó. |

### Pagos

- **Suscripción de barbería**: mensual adelantada, sin días gratis. Se simula/mock inicialmente; MercadoPago se integra en una fase posterior.
- **Pagos de citas**: el cliente paga en la barbería mediante **transferencia** o **efectivo**. El sistema registra el pago pero no procesa pasarela en línea en las primeras fases.

### Notas adicionales de decisión

- El agendamiento de citas se implementa **después** de que la barbería esté configurada y activada.
- Las notificaciones por email se implementan junto con el agendamiento; si es sencillo se hacen de una vez.
- El cliente **debe estar registrado** para agendar; no hay agendamiento como invitado.
- El admin **no agenda citas por teléfono** en esta versión; el flujo es self-service por el cliente.
- Los barberos pueden tener **horarios específicos** diferentes al horario general de la barbería.
- Desde el inicio se valida que **no haya citas solapadas** para un mismo barbero.

### Identificación pública de barbería

- Cada barbería tiene un **slug único** elegido por el dueño (por ejemplo, `los-pinos`).
- URLs amigables: `legacybarber.app/b/los-pinos`.
- Si el slug ya existe, se rechaza y se pide otro.

### Contexto de barbería en requests

- Para clientes, el JWT indica los roles y, cuando aplica, la barbería activa.
- Si un cliente pertenece a varias barberías, el frontend envía un header `X-Barberia-Id` para indicar en cuál está operando.
- Barberos y admins operan siempre dentro de su barbería asignada.

### Notas adicionales

- Un **admin puede ser barbero** simultáneamente (roles `admin` + `barbero`).
- Un **barbero puede ser cliente de otra barbería** técnicamente en el backend; el frontend no soportará ese caso en la primera versión.
- El marketplace muestra solo barberías en estado `Activa`.

### Correcciones de seguridad y calidad (julio 2026)

Durante una auditoría de Fase 1 se identificaron y corrigieron los siguientes puntos:

1. **Normalización de roles** (`RolRepository.cs`): ahora aplica `Trim().ToLowerInvariant()` para evitar que roles con mayúsculas queden sin asignar.
2. **Validación del header `X-Barberia-Id`** (`CurrentUserService.cs`): el header se contrasta contra las relaciones reales del usuario (tablas `Clientes`, `Barbero`, `Usuario.BarberiaId`) antes de aceptarlo. Un superadmin tiene acceso a cualquier barbería.
3. **Restricción de rol en `asociar-barberia`** (`ClientesController.cs`): solo usuarios con rol `cliente` pueden asociarse a una barbería.
4. **Email duplicado controlado** (`UsuarioService.CreateAsync`): ahora verifica el email antes de insertar y devuelve un error 409 claro.
5. **Superadmin con email verificado** (`DatabaseSeeder.cs`): `EmailVerificado = true` para evitar bloqueo cuando se active la verificación obligatoria (Fase 14).
6. **`LoginResponseModel.Usuario.Barberias`** ahora incluye la lista de barberías del usuario (antes solo estaba en `LoginResponseModel.Barberias`).
7. **`UsersController` renombrado a `UsuariosController`** para consistencia con el resto de la nomenclatura en español.
8. **`UsuarioModel.Barberias`** agregado para que el superadmin vea a qué barberías pertenece cada usuario en `/api/usuarios`.

## Roadmap / Plan de fases

El desarrollo se divide en fases secuenciales. Cada fase se implementa y prueba antes de pasar a la siguiente.

### Fase 1: Autenticación y onboarding de clientes ✅
- Ajustar modelo de datos (`Barberia.Slug`, quitar índice único de `Cliente.UsuarioId`).
- Implementar registro global de cliente (nombre, email, teléfono, contraseña).
- Implementar login unificado.
- Endpoint público para listar barberías disponibles (`GET /api/barberias/disponibles`).
- Endpoint para que un cliente se asocie a una barbería (`POST /api/clientes/asociar-barberia`).
- Ajustar JWT para soportar contexto de barbería.

### Fase 2: Onboarding de barberías ✅
- Registro de barbería por dueño (self-service) (`POST /api/barberias/mias`).
- Campos: nombre, slug, dirección, ciudad, teléfono, email de contacto y logo (opcional).
- Slug validado con regex `^[a-z0-9]+(-[a-z0-9]+)*$` y verificado como único en BD.
- Estado inicial `EnConfiguracion`.
- El dueño es ascendido automáticamente a `admin` de su barbería (`Usuario.BarberiaId` + rol `admin`).
- El endpoint de creación devuelve `LoginResponseModel` con tokens nuevos (roles y `barberia_id` actualizados).
- Endpoint para ver la barbería del usuario actual (`GET /api/barberias/mias`).
- Configuración de horario de atención general (`PUT /api/barberias/mias/horario`).

### Fase 3: Gestión de catálogo (admin)
- CRUD de categorías de servicios.
- CRUD de servicios: nombre, descripción, duración, precio, imagen, categoría.
- Activar/desactivar servicios.

### Fase 4: Gestión de barberos (admin)
- CRUD de barberos.
- Crear `Usuario` con rol `barbero` y entidad `Barbero` vinculada a la barbería del admin.
- Especialidades, biografía, foto, comisión.
- Asociar servicios que atiende cada barbero.
- Validar que el admin solo administre su propia barbería.

### Fase 5: Configuración de barbería (admin)
- Horarios de atención de la barbería.
- Excepciones de horario (días cerrados, festivos).
- Métodos de pago aceptados (transferencia, efectivo).
- Datos de contacto y perfil público.

### Fase 6: Activación y pagos de barbería
- Pago simulado/mock de suscripción mensual.
- Al completar el pago, la barbería pasa a estado `Activa`.
- Aparece en el marketplace y puede recibir citas.

### Fase 7: Dashboard del barbero
- Panel con citas del día.
- Mi agenda (calendario personal).
- Mis clientes.
- Mis reseñas.
- Ingresos básicos.

### Fase 8: Agendamiento de citas (cliente)
- Seleccionar servicio(s).
- Seleccionar barbero.
- Ver disponibilidad de horarios respetando el horario de la barbería y del barbero.
- Seleccionar fecha y hora.
- Confirmar cita.
- Guardar cita en estado `pendiente`.
- Validar que no haya citas solapadas para el mismo barbero.

### Fase 9: Gestión de citas (admin y barbero)
- Ver citas de la barbería.
- Confirmar, reprogramar, cancelar y completar citas.
- Estados de cita: pendiente, confirmada, en_curso, completada, cancelada_*.

### Fase 10: Notificaciones
- Bandeja de notificaciones del usuario.
- Recordatorios de cita por email.
- Notificaciones de confirmación/cancelación.
- (Futuro: WhatsApp.)

### Fase 11: Perfil y cuenta de usuario
- Editar datos personales.
- Cambiar contraseña.
- Preferencias de notificación.
- Ver mis barberías.

### Fase 12: Reseñas
- Cliente deja reseña después de cita completada.
- Calificación y comentario.
- Ver reseñas en perfil de barbero.
- Calificación promedio.

### Fase 13: Pagos de citas
- Registrar método de pago de la cita (transferencia o efectivo).
- Registrar que la cita fue pagada.
- Historial de pagos por cita.

### Fase 14: Seguridad y verificación
- Verificación de email obligatoria.
- Recuperación de contraseña.
- Token de verificación.

### Fase 15: Reportes y métricas (admin)
- Dashboard con KPIs.
- Citas atendidas.
- Ingresos.
- Clientes nuevos.
- Servicios más solicitados.

### Fase 16: Integración real de pagos
- Integrar MercadoPago para suscripciones de barbería.
- Integrar MercadoPago para pagos de citas.
- Webhooks para confirmar pagos y renovaciones.

## Pendientes conocidos

### Fases de funcionalidad
- [x] Fase 1: autenticación y onboarding de clientes.
- [x] Fase 2: onboarding de barberías.
- [ ] Fase 3: gestión de catálogo (admin).
- [ ] Fase 4: gestión de barberos (admin).
- [ ] Fase 5: configuración de barbería (admin).
- [ ] Fase 6: activación y pagos de barbería.
- [ ] Fase 7: dashboard del barbero.
- [ ] Fase 8: agendamiento de citas (cliente).
- [ ] Fase 9: gestión de citas (admin y barbero).
- [ ] Fase 10: notificaciones.
- [ ] Fase 11: perfil y cuenta de usuario.
- [ ] Fase 12: reseñas.
- [ ] Fase 13: pagos de citas.
- [ ] Fase 14: seguridad y verificación de email.
- [ ] Fase 15: reportes y métricas.
- [ ] Fase 16: integración real de pagos con MercadoPago.

### Mejoras técnicas transversales
- [ ] Mover infraestructura de seguridad (`JwtTokenService`, `AspNetPasswordHasher`) fuera de `App.Api`.
- [ ] Implementar paginación y filtrado por `BarberiaId` en listados.
- [ ] Agregar transacciones explícitas a `IUnitOfWork` si se requiere atomicidad multi-repositorio.
- [ ] Configurar Swagger con esquema Bearer.
- [x] Agregar rate limiting y CORS (CORS configurado para `localhost:3000`, rate limiting pendiente).
- [ ] Implementar constraint `EXCLUDE` de PostgreSQL para evitar citas solapadas.
- [ ] Crear proyectos de pruebas unitarias e integración.
