# Pontificia Universidad Javeriana
# Laboratorio 1 – Implementación de Monolito con patrón MVC y DAO
# Arquitectura de Software
# Andrés Calderón
# Juan Camilo Carvajal y Gabriela Rojas
# 2025

# Repositorio: personaapi-dotnet

Implementación de una API REST en .NET 8 con patrón MVC y DAO.  
Este laboratorio desarrolla un CRUD completo para las entidades **Persona**, **Profesión**, **Estudio** y **Teléfono**, utilizando **SQL Server 2022** y **Entity Framework Core**.

---

## Stack Tecnológico

- .NET 8.0  
- Microsoft SQL Server 2022  
- Entity Framework Core  
- Swagger (OpenAPI 3.0)
- Visual Studio Community 2022

---

## Descripción General

El laboratorio consiste en la implementación de un sistema monolítico que sigue el patrón MVC y utiliza el modelo DAO para manejar la persistencia de datos.  
Se emplea **Entity Framework Core** como ORM y **Swagger** para exponer y probar los endpoints REST.

---

## Estructura del Proyecto
```
personaapi-dotnet/
│
├── Controllers/
│ ├── PersonaController.cs
│ ├── EstudioController.cs
│ ├── ProfesionController.cs
│ └── TelefonoController.cs
│
├── Models/
│ └── Entities/
│ ├── Persona.cs
│ ├── Estudio.cs
│ ├── Profesion.cs
│ ├── Telefono.cs
│ └── PersonaDbContext.cs
│
├── Repositories/
│ ├── IRepository.cs
│ └── Repository.cs
│
├── Scripts/
│ └── ddl_persona_db.sql
│
├── Program.cs
├── appsettings.json
├── README.md
└── .gitignore
```
---

## Configuración del Ambiente

1. Clonar el repositorio desde GitHub:
   ```bash
   git clone https://github.com/Gaby27r/personaapi-dotnet.git
   ```
2. Abrir la solución en Visual Studio 2022.

3. Verificar las dependencias NuGet:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Swashbuckle.AspNetCore

4. Configurar la cadena de conexión en el archivo appsettings.json:

"ConnectionStrings": {
  "ConexionSQL": "Server=localhost\\SQLEXPRESS;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=True;"
}

## Compilación
1. Abrir el proyecto en **Visual Studio 2022**.
2. Verificar que el proyecto `personaapi-dotnet` esté configurado como proyecto de inicio.
3. Seleccionar el perfil **https** en la barra superior.
4. Presionar **Ctrl + F5** o el botón “Ejecutar sin depurar”.
5. Esperar a que Visual Studio compile la solución (sin errores).

## Despliegue Local
1. Una vez compilado, el proyecto abrirá automáticamente **Swagger UI** en el navegador.
2. Swagger se abrirá automáticamente en la dirección:
https://localhost:7158/swagger/index.html
