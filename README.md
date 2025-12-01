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
└── Infrastructure/  # Servicios externos
```

## 📚 Endpoints principales

### Autenticación
- POST `/api/auth/login`
- POST `/api/auth/register`

### Veterinarias
- GET `/api/veterinarias/mis-veterinarias`
- POST `/api/veterinarias`
- PUT `/api/veterinarias/{id}`

### Mascotas
- GET `/api/mascotas`
- POST `/api/mascotas`
- PUT `/api/mascotas/{id}`
- DELETE `/api/mascotas/{id}`

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

Tu Nombre - [GitHub](https://github.com/tuusuario)
