# Prueba Técnica - Gestión de Vehículos e Inspecciones

Aplicación para la administración de vehículos e inspecciones técnicas.

La solución permite registrar, consultar, actualizar y desactivar vehículos, además de registrar y consultar las inspecciones asociadas a cada vehículo.

---

## Tecnologías

* C#
* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* Swagger
* xUnit

---

# Arquitectura

La solución está organizada por responsabilidades.

```text
src/
│
├── Vehiculos.Api/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Filters/
│   ├── Middleware/
│   ├── Services/
│   ├── Result.cs
│   └── Program.cs
│
├── Vehiculos.Domain/
│   ├── Entities/
│   └── Enums/
│
└── Vehiculos.Infrastructure/
    ├── Persistence/
    │   └── Configurations/
    └── Repositories/
        └── Interfaces/
```

---

# Conexión Base de Datos

La conexión a PostgreSQL se configura en el archivo `appsettings.json`.

Ejemplo de conexión local:

```text
Host=localhost;Port=5432;Database=database;Username=postgres;Password=password
```

Los valores deben ser modificados de acuerdo con la configuración local de PostgreSQL.

---

## Migraciones de Base de Datos

La estructura de la base de datos se administra mediante scripts SQL versionados.

Las migraciones se encuentran en:

```text
Vehiculos.Infrastructure/
└── Persistence/
    └── Migration/
        └── V01_0_0__Inicial.sql
```

Cada cambio realizado en la estructura de la base de datos debe generar una nueva migración.

---

# Como Ejecutar

Primero se debe tener PostgreSQL ejecutándose y configurar la cadena de conexión en `appsettings.json`.

Crear la base de datos:

```sql
CREATE DATABASE vehiculos_inspecciones;
```

Después ejecutar el script de migración inicial:

```text
Vehiculos.Infrastructure/Persistence/Migration/V01_0_0__Inicial.sql
```

Luego ejecutar los siguientes comandos desde la raíz del proyecto:

```bash
dotnet restore
dotnet build
dotnet run
```

Para ejecutar las pruebas:

```bash
dotnet test
```

---

# Swagger

Una vez iniciada la aplicación, Swagger estará disponible en:

```text
http://localhost:xxxx/swagger/index.html
```

El puerto `xxxx` corresponde al puerto configurado para la aplicación.

Desde Swagger se pueden consultar y probar los endpoints disponibles de la API.

---

# Endpoints

La API permite realizar las siguientes operaciones:

### Vehículos

* Registrar vehículo.
* Consultar vehículos.
* Consultar vehículo por ID.
* Actualizar vehículo.
* Desactivar vehículo.

### Inspecciones

* Registrar inspección.
* Consultar inspecciones asociadas a un vehículo.

Los endpoints y sus respectivos parámetros pueden ser consultados directamente desde Swagger.


