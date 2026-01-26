# 🏥 Yolcatl — Sistema de Gestión Veterinaria

## Descripción General

**Yolcatl** (Náhuatl: "Animal con corazón") es una API REST para la gestión integral de veterinarias, clientes y mascotas. Construida con **.NET 9**, **Entity Framework Core** y **PostgreSQL**, sigue un patrón de arquitectura **N-Layer** que garantiza separación de responsabilidades, testabilidad y escalabilidad.

La API proporciona funcionalidades de autenticación JWT, gestión de veterinarias, clientes y mascotas, así como consultas veterinarias.

---

## 📋 Objetivos del Proyecto

1. **Centralizar la gestión de veterinarias**: permitir que administradores de veterinarias registren, actualicen y gestionen su información.
2. **Administrar clientes y mascotas**: asociar clientes a veterinarias y mascotas a clientes, con auditoría completa.
3. **Registrar consultas**: documentar consultas veterinarias con trazabilidad.
4. **Seguridad**: autenticación y autorización mediante JWT; validar propiedad y permisos en cada operación.
5. **Escalabilidad**: arquitectura modular, inyección de dependencias, repositorio pattern y soft delete para historial.

---

## 🏗️ Estructura de Proyecto
API-Veterinaria/ ├── Controllers/                # Endpoints HTTP (entrada) │   ├── UsuariosController.cs          # Login, registro, renovación de token │   ├── VeterinariasController.cs      # CRUD veterinarias │   ├── ClienteController.cs           # CRUD clientes │   ├── MascotaController.cs           # CRUD mascotas │   └── ConsultaController.cs          # Registrar consultas ├── Business/                   # Lógica de negocio (orquestación) │   ├── Interfaces/ │   │   ├── IVeterinariaService.cs │   │   ├── IClienteService.cs │   │   ├── IMascotaService.cs │   │   ├── IConsultaService.cs │   │   └── IUsuarioService.cs │   └── Services/ │       ├── VeterinariaService.cs │       ├── ClienteService.cs │       ├── MascotaService.cs │       ├── ConsultaService.cs │       └── UsuarioService.cs ├── Data/                       # Acceso a datos (repositorio pattern) │   ├── VeterinariaDbContext.cs       # DbContext de EF Core │   ├── Interfaces/ │   │   └── I*Repository.cs │   └── Repositories/ │       ├── ClienteRepository.cs │       ├── MascotaRepository.cs │       ├── VeterinariaRepository.cs │       └── ConsultaRepository.cs ├── Core/                       # Modelos y DTOs (núcleo de dominio) │   ├── Entities/ │   │   ├── Usuario.cs │   │   ├── Veterinaria.cs │   │   ├── Cliente.cs │   │   ├── Mascota.cs │   │   ├── Consulta.cs │   │   └── IAuditable.cs (interfaz) │   └── DTOs/ │       ├── Autenticacion/ │       ├── Veterinaria/ │       ├── Cliente/ │       ├── Mascota/ │       └── Consulta/ ├── Exceptions/                 # Excepciones personalizadas │   ├── NotFoundException.cs │   ├── ForbidenException.cs │   └── NotActiveException.cs ├── Extensions/                 # Extensiones de servicios │   ├── IdentityServiceExtensions.cs │   └── ApplicationServiceExtensions.cs ├── Migrations/                 # EF Core migrations ├── Program.cs                  # Configuración e inyección de dependencias ├── appsettings.json            # Configuración (base de datos, JWT, etc.) └── README.md      

---

## 🔄 Flujo de Arquitectura N-Layer
HTTP Request ↓ [Controllers] — parsea entrada, valida atributos ↓ [Business Services] — valida reglas, orquesta repositorios, autoriza ↓ [Data Repositories] — consulta BD con EF Core ↓ [Database - PostgreSQL] ↑ [DTOs / Mappers] — convierte Entities ↔ DTOs (AutoMapper) ↓ HTTP Response (200, 201, 204, 400, 403, 404, 500)

---

## 📚 Entidades Principales

### 1. **Usuario**
- Autenticación con Identity
- JWT para sesiones sin estado
- Propiedades: Email, PasswordHash, etc.

### 2. **Veterinaria**
- Dueño: Usuario (relación 1:N)
- Propiedades: Nombre, Dirección, Teléfono, Email
- Soft delete (FechaBaja)

### 3. **Cliente**
- Asociado a Veterinaria (relación N:1)
- Propiedades: Nombre, Apellidos, Email, Teléfono
- Soft delete

### 4. **Mascota**
- Asociada a Cliente (relación N:1)
- Propiedades: Nombre, Especie, Sexo, Peso, FechaNacimiento, Observaciones
- Soft delete

### 5. **Consulta**
- Registra visitas de mascotas
- Auditoría de cambios

---

## 🔐 Flujo de Seguridad

1. **Autenticación** (sin token):
   - POST `/api/usuarios/iniciar-sesion` → válida credenciales → devuelve JWT
   - POST `/api/usuarios/registro` → crea usuario → devuelve JWT

2. **Autorización** (con token):
   - Header: `Authorization: Bearer {token}`
   - Toda petición protegida valida el token y sus claims

3. **Control de Acceso**:
   - Veterinaria: solo el propietario (usuario) puede verla/editarla
   - Cliente: solo usuarios de esa veterinaria
   - Mascota: solo clientes/usuarios asociados

---

## 🔌 Endpoints Principales

### Autenticación
POST   /api/usuarios/iniciar-sesion      → Login 
POST   /api/usuarios/registro            → Registro GET    
/api/usuarios/renovar-token              → Renovar token (requiere [Authorize])

### Veterinarias
GET    /api/veterinarias                 → Listar todas (TODO: revisar permiso) 
GET    /api/veterinarias/{id}            → Obtener por id 
GET    /api/veterinarias/mi-veterinaria  → Obtener la del usuario autenticado 
POST   /api/veterinarias                 → Crear 
PUT    /api/veterinarias/{id}            → Actualizar 
DELETE /api/veterinarias/{id}            → Activar/Desactivar (soft delete)

### Clientes
GET    /api/clientes                     → Listar todos (requiere [Authorize]) 
GET    /api/clientes/{id}                → Obtener cliente por id (requiere [Authorize]) 
POST   /api/clientes                     → Crear cliente (requiere [Authorize]) 
PUT    /api/clientes/{id}                → Actualizar cliente por id (requiere [Authorize]) 
DELETE /api/clientes/{id}                → Eliminar cliente por id (requiere [Authorize])

### Mascotas
GET    /api/mascotas                     → Listar todas (requiere [Authorize]) 
GET    /api/mascotas/{id}                → Obtener mascota por id (requiere [Authorize]) 
POST   /api/mascotas                     → Crear mascota (requiere [Authorize]) 
PUT    /api/mascotas/{id}                → Actualizar mascota por id (requiere [Authorize]) 
DELETE /api/mascotas/{id}                → Eliminar mascota por id (requiere [Authorize])

### Consultas
GET    /api/consultas                   → Listar todas (requiere [Authorize]) 
GET    /api/consultas/{id}               → Obtener consulta por id (requiere [Authorize]) 
POST   /api/consultas                   → Registrar consulta (requiere [Authorize]) 
PUT    /api/consultas/{id}               → Actualizar consulta por id (requiere [Authorize]) 
DELETE /api/consultas/{id}               → Eliminar consulta por id (requiere [Authorize])

---

## 💡 Ejemplos de Uso

### Crear Veterinaria
```http
POST /api/veterinarias
Content-Type: application/json
Authorization: Bearer {token}

{
  "nombre": "Clínica Veterinaria San José",
  "direccion": "Av. Siempre Viva 742",
  "telefono": "555987654321",
  "email": "contacto@veterinariasanjose.com"
}
```

### Error 403 - Acceso denegado
```http
HTTP/1.1 403 Forbidden
Content-Type: application/json

{
  "error": "No tienes permiso para realizar esta operación."
}
```

---

## 🛠️ Tecnologías

| Capa | Tecnología | Versión |
|------|-----------|---------|
| Runtime | .NET | 9.0 |
| Web | ASP.NET Core | 9.0 |
| ORM | Entity Framework Core | 9.0.9 |
| BD | PostgreSQL | 14+ |
| Driver BD | Npgsql | 9.0.3 |
| Mapeo | AutoMapper | 14.0.0 |
| Auth | JWT Bearer | 9.0.9 |
| Documentación | Swagger (Swashbuckle) | 6.4.0 |
| Anotaciones | Swashbuckle.Annotations | 6.4.0 |

---

## 🚀 Requisitos e Instalación

### Requisitos
- **.NET 9 SDK** (descargar desde [dotnet.microsoft.com](https://dotnet.microsoft.com))
- **PostgreSQL 14+**
- **Visual Studio 2022** o **VS Code**
- **Git**

### Pasos de Instalación

1. **Clonar repositorio**:
1. git clone https://github.com/Hector984/api-backend-veterinaria.git cd api-backend-veterinaria
2. **Restaurar paquetes**: dotnet restore
3. **Configurar base de datos** (appsettings.json):
1. { "ConnectionStrings": { "DefaultConnection": "Host=localhost;Database=VeterinariaDB;Username=postgres;Password=tu_contraseña" }, "llavejwt": "tu_clave_secreta_jwt_muy_larga_y_segura" }
4. **Ejecutar migraciones**: dotnet ef database update
5. **Ejecutar**: dotnet run

La API estará disponible en `https://localhost:5000` (o el puerto configurado).

6. **Swagger** (documentación interactiva):

---

## 🧪 Testing (Recomendado)

Se recomienda crear tests unitarios para:
- Servicios de negocio (IVeterinariaService, IClienteService, etc.)
- Validaciones de autorización
- Manejo de excepciones

Herramientas sugeridas:
- **xUnit** o **MSTest**
- **Moq** (para mocking de dependencias)
- **FluentAssertions** (para aserciones claras)

Estructura:
API-Veterinaria.Tests/ ├── Business/ │   ├── VeterinariaServiceTests.cs │   ├── ClienteServiceTests.cs │   └── ... └── Controllers/ ├── VeterinariasControllerTests.cs └── ...

---

## 📋 Buenas Prácticas Implementadas

✅ **Arquitectura N-Layer**: separación clara entre Controllers, Business, Data  
✅ **Inyección de Dependencias**: servicios registrados en Program.cs  
✅ **Repository Pattern**: abstracción del acceso a datos  
✅ **DTOs**: mapeo entre Entities y Transfer Objects  
✅ **AutoMapper**: configuración de mapeos centralizados  
✅ **Soft Delete**: auditoría mediante FechaBaja en lugar de eliminación física  
✅ **Validación de entrada**: DataAnnotations en DTOs  
✅ **Manejo de excepciones**: excepciones de dominio (NotFoundException, ForbidenException)  
✅ **Autenticación JWT**: stateless, escalable  
✅ **Autorización**: validación de propiedad y permisos en servicios  
✅ **Swagger/OpenAPI**: documentación automática y testing interactivo  

---

## 🔄 Ciclo de Desarrollo

1. **Feature branch**: crear rama `feature/nombre-funcionalidad`
2. **Implementar**: Controllers → Services → Repositories
3. **Documentar**: comentarios XML, atributos SwaggerOperation
4. **Tests**: unitarios en caso de lógica crítica
5. **PR & Review**: merge a `main` tras aprobación
6. **Deploy**: CD en staging/producción (GitHub Actions recomendado)

---

## 📝 Pautas de Contribución

- Mantener el patrón N-Layer
- Usar PascalCase para clases/métodos, camelCase para variables locales
- Campos privados: `_camelCase`
- Métodos asincronos: sufijo `Async`
- Excepciones de dominio en lugar de excepciones framework cuando sea posible
- Documentar en Swagger con `[SwaggerOperation]` y `[ProducesResponseType]`

Véase `CONTRIBUTING.md` para más detalles.

---

## 🔗 Recursos

- [Documentación .NET 9](https://learn.microsoft.com/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [JWT Authentication en ASP.NET Core](https://learn.microsoft.com/aspnet/core/security/authentication/)
- [Swashbuckle / Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [PostgreSQL](https://www.postgresql.org/)

---

## 👤 Autor

**Héctor Antonio Jiménez Manzo**  
📧 hectorantoniojimenezmanzo@gmail.com  
🔗 [LinkedIn](https://www.linkedin.com/in/h%C3%A9ctor-antonio-jim%C3%A9nez-manzo/)  
🐙 [GitHub](https://github.com/Hector984)

---

## 📄 Licencia

MIT — Libra usar, modificar y distribuir con atribución.

---

**Última actualización**: enero 26, 2026  
**Estado**: En desarrollo activo