# Incident Management API
API REST desarrollada en **ASP.NET Core 8**, diseñada para gestionar incidentes y sus comentarios asociados.  
Incluye middleware de excepciones, logging técnico, Entity Framework Core con SQLite y pruebas unitarias con xUnit.

## 🚀 Tecnologías utilizadas
- ASP.NET Core 8 (Web API)
- Entity Framework Core 8 + SQLite
- EF Core InMemory para pruebas
- xUnit para pruebas unitarias
- Inyección de dependencias (DI)
- Logging con ILogger
- Swagger / OpenAPI

## 📌 Arquitectura del proyecto

### ✔ Controllers (Presentación)
Reciben solicitudes HTTP y delegan la lógica a los servicios.

### ✔ DTOs (Data Transfer Objects)
Separan los modelos expuestos al cliente de las entidades de base de datos.

### ✔ Services (Lógica de negocio)
Encapsulan la lógica principal:
- `IncidentService`
- `CommentService`

### ✔ Data (Infraestructura)
- `AppDbContext`
- Configuración de EF Core
- DbSets de `Incidents` y `Comments`

### ✔ Models (Dominio)
Representan las entidades persistidas:
- `Incident`
- `Comment`

---

## 🛡 Middleware global de excepciones
Middleware personalizado que:
- Intercepta errores no controlados
- Registra el error
- Devuelve un JSON consistente

Ejemplo de respuesta:
```json
{
  "error": "Ha ocurrido un error inesperado.",
  "traceId": "0H123ABC..."
}
```

---

## 📘 Logging técnico
Se utiliza `ILogger<T>` para registrar:
- Creación, actualización y eliminación de recursos
- Advertencias por recursos inexistentes
- Errores procesados por el middleware

---

## 🧪 Pruebas unitarias
Implementadas en el proyecto **IncidentManagement.Tests** usando:
- xUnit
- Entity Framework Core InMemory

### Pruebas realizadas

#### IncidentServiceTests
- ✔ Crear incidente (camino feliz)
- ✔ Error al actualizar incidente inexistente

#### CommentServiceTests
- ✔ Crear comentario asociado
- ✔ Error si el incidente no existe

Todas las pruebas pasan exitosamente.

---

## 🗄 Base de datos
Se utiliza **SQLite**.

Cadena de conexión en `Program.cs`:
```csharp
options.UseSqlite("Data Source=incidents.db")
```

Migraciones:
```
Add-Migration InitialCreate
Update-Database
```

---

## 🔧 Cómo ejecutar el proyecto
### 1. Restaurar dependencias
```
dotnet restore
```

### 2. Aplicar migraciones
```
dotnet ef database update
```

### 3. Ejecutar la API
```
dotnet run
```

### 4. Abrir Swagger
```
https://localhost:7045/swagger
```

---

## 📡 Endpoints principales

### Incidentes
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Incidents` | Listar incidentes |
| GET | `/api/Incidents/{id}` | Obtener por ID |
| POST | `/api/Incidents` | Crear incidente |
| PUT | `/api/Incidents/{id}` | Actualizar incidente |
| DELETE | `/api/Incidents/{id}` | Eliminar incidente |

### Comentarios
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Incidents/{id}/comments` | Obtener comentarios de un incidente |
| POST | `/api/Incidents/{id}/comments` | Agregar comentario |

---

## 📂 Estructura del proyecto
```
IncidentManagement.API/
│
├── Controllers/
├── Data/
├── Dtos/
├── Middleware/
├── Models/
├── Services/
└── Program.cs
```

---

## ✔ Notas finales
Este proyecto implementa:
- Buenas prácticas de arquitectura
- Manejo robusto de errores
- Logging técnico
- Pruebas unitarias reales
- Persistencia con EF Core
- Documentación con Swagger

Listo para revisión o ampliación.

