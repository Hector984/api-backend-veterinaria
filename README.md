# 🏥 Sistema de Gestión Veterinaria

API REST para gestión de veterinarias, clientes y mascotas construida con .NET 9 y PostgreSQL.

## 🚀 Características

- ✅ Autenticación JWT
- ✅ Arquitectura N-Layer (Controller → Business → Data)
- ✅ Soft Delete
- ✅ AutoMapper
- ✅ Entity Framework Core
- ✅ PostgreSQL

## 📋 Requisitos

- .NET 9 SDK
- PostgreSQL 14+
- Visual Studio 2022 / VS Code

## ⚙️ Instalación
```bash
# Clonar repositorio
git clone https://github.com/tuusuario/veterinaria-api.git

# Restaurar paquetes
dotnet restore

# Configurar cadena de conexión en appsettings.json
# Ejecutar migraciones
dotnet ef database update

# Ejecutar
dotnet run
```

## 🏗️ Arquitectura
```
VeterinariaAPI/
├── Controllers/      # Endpoints API
├── Business/         # Lógica de negocio
├── Data/            # Repositorios y DbContext
├── Core/            # Entidades y DTOs
```

## 📚 Endpoints principales

### Autenticación
- POST `/api/usuarios/iniciar-sesion`
- POST `/api/usuarios/registro`
- GET `/api/usuarios/renovar-token`

### Veterinarias
- POST `/api/veterinarias`
- GET `/api/veterinarias`
- GET `/api/veterinarias/{id}`
- GET `/api/veterinarias/mi-veterinaria`
- PUT `/api/veterinarias/{id}`
- DELETE `/api/veterinarias/{id}`

### Clientes
- POST `/api/clientes`
- GET `/api/clientes/{id}`
- GET `/api/clientes/veterinaria/{id}`
- PUT `/api/clientes/{id}`
- DELETE `/api/clientes/{id}`
  
### Mascotas
- POST `/api/mascotas`
- GET `/api/mascotas/{id}`
- GET `/api/mascotas/cliente/{id}`
- GET `/api/mascotas/veterinaria/{id}`
- PUT `/api/mascotas/{id}`
- DELETE `/api/mascotas/{id}`

### Consultas
- POST `/api/consultas`

## 🔐 Autenticación

Todas las peticiones (excepto login/register) requieren token JWT:
```
Authorization: Bearer {token}
```

## 🛠️ Tecnologías

- .NET 9
- Entity Framework Core 9
- PostgreSQL
- AutoMapper
- JWT Authentication
- Npgsql

## 📝 Licencia

MIT

## 👤 Autor

Héctor Antonio Jiménez Manzo - [GitHub](https://github.com/tuusuario)
