# Todo-List-API 🇬🇧
Build a RESTful API to allow users to manage their to-do list.

Backend project proposal for [roadmap.sh](https://roadmap.sh). Full project details can be found at [Todo List API](https://roadmap.sh/projects/todo-list-api)  

Developed by Carlos Blanco. 📧acbgalan@gmail.com  🌐 www.carlosblanco.dev

### 🚀 Technologies used
- ASP.NET Core 8 Web API
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- AutoMapper v15

### 📚 Topics Covered
This project addresses a wide range of backend development topics and architectural practices, including:

- RESTful API
  - Fully structured REST API exposing endpoints for core application functionality.
  - Supports filtering, sorting, and pagination for efficient data querying

- Data Transfer & Mapping
  - Uses DTOs (Data Transfer Objects) to decouple internal models from exposed API contracts.
  - AutoMapper is integrated to streamline object-to-object mapping between entities and DTOs.

- Data Access
  - Built on Entity Framework Core for ORM and database interaction.
    - Implements Repository pattern to abstract and manage data access logic.

- Authentication & Security
  - Secured with JWT (JSON Web Tokens) for stateless authentication.
  - Integrated with ASP.NET Core Identity for user management, roles, and claims-based authorization.

- Design Patterns & Architecture
  - Applies Service Pattern to encapsulate business logic.
  - Uses a Wrapper Response model to standardize API responses.
  - Employs Dependency Injection throughout the application for loose coupling and testability, following the Dependency Inversion Principle.

- Testing & Documentation
  - API endpoints are documented and testable via:
    - Swagger UI (auto-generated OpenAPI docs)
    - Postman collections for manual and automated testing (Not provided)
- DevOps & Tooling
  - Git for version control, using git flow branching.

### 🚦 Getting Started
- Restore NuGet packages
- Run Entity Framework migrations to create the database
- Make sure the startup project is BloggingPlatform.Server (Web API)
- Run the API from Visual Studio 2022 to test its functionality
- Configure AutoMapper License (optional)

  Get your free automapper licence from [automapper.io](https://automapper.io/) 

  Provide your license key using User Secrets.
  
  ```json
  {
    "AutoMapper": {
      "LicenseKey": "your key"
    }
  }
  ```  

### 🧪 Usage
This project provides comprehensive API documentation via an integrated Swagger UI.
You can explore available endpoints, request/response schemas, and test API calls directly from the browser

### 🏭 Project Structure

📁 BloggingPlatform.Data  
├── 📂 Context          - Entity Framework context  
├── 📂 Entities         - Domain entities  
├── 📂 Migrations       - Database migrations  
└── 📂 Repositories     - Data access repositories  

📁 BloggingPlatform.Server  
├── 📂 Controllers      - Controllers with endpoints  
└── 📂 Mapper           - AutoMapper configurations  
└── 📂 Services         - Business logic encapsulated within services  

📁 BloggingPlatform.Shared  
├── 📂 Todo         - DTOs for Todo items  
└── 📂 User         - DTOs for User items 

---

# Todo-List-API 🇪🇸
Construye una API RESTful que permita a los usuarios gestionar su lista de tareas.

Propuesta de proyecto backend para [roadmap.sh](https://roadmap.sh). Los detalles completos del proyecto están disponibles en [Todo List API](https://roadmap.sh/projects/todo-list-api)  

Desarrollado por Carlos Blanco. 📧acbgalan@gmail.com  🌐 www.carlosblanco.dev

### 🚀 Tecnologías utilizadas
- ASP.NET Core 8 Web API
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- AutoMapper v15

### 📚 Temas abordados
Este proyecto cubre una amplia gama de temas de desarrollo backend y prácticas arquitectónicas, incluyendo:

- RESTful API
  - API REST completamente estructurada que expone endpoints para la funcionalidad principal de la aplicación.
  - Soporta filtrado, ordenamiento y paginación para consultas de datos eficientes.

- Transferencia y mapeo de datos
  - Utiliza DTOs (Objetos de Transferencia de Datos) para desacoplar los modelos internos de los contratos expuestos por la API.
  - AutoMapper está integrado para facilitar el mapeo entre entidades y DTOs.

- Accesso a datos
  - Construido sobre Entity Framework Core para ORM e interacción con la base de datos.
  - Implementa el patrón Repositorio para abstraer y gestionar la lógica de acceso a datos.

- Autenticación y seguridad
  - Protegido con JWT (JSON Web Tokens) para autenticación sin estado.
  - Integrado con ASP.NET Core Identity para gestión de usuarios, roles y autorización basada en claims.

- Patrones de diseño y arquitectura
  - Aplica el patrón de Servicio para encapsular la lógica de negocio.
  - Emplea Inyección de Dependencias en toda la aplicación para un acoplamiento débil y facilidad de pruebas, siguiendo el Principio de Inversión de Dependencias.    

- Pruebas y documentación
  - Los endpoints de la API están documentados y son testeables mediante:
    - Swagger UI (documentación OpenAPI generada automáticamente)
    - Colecciones de Postman para pruebas manuales y automatizadas (no proporcionadas)
      
- DevOps & herramientas
  - Git para control de versiones, utilizando ramificación con git flow.

### 🚦 Primeros pasos
- Restaurar los paquetes NuGet
- Ejecutar las migraciones de Entity Framework para crear la base de datos
- Asegurarse de que el proyecto de inicio sea BloggingPlatform.Server (Web API)
- Ejecutar la API desde Visual Studio 2022 para probar su funcionalidad
- Configurar la licencia de AutoMapper (opcional)

  Obtén tu licencia gratuita de automapper dese [automapper.io](https://automapper.io/) 

  Introduce tu licencia mediante User Secrets.
  
  ```json
  {
    "AutoMapper": {
      "LicenseKey": "your key"
    }
  }
  ```  

### 🧪 Uso
Este proyecto proporciona documentación completa de la API mediante una interfaz integrada de Swagger UI.
Puedes explorar los endpoints disponibles, los esquemas de solicitud/respuesta y probar llamadas a la API directamente desde el navegador.

### 🏭 Estructura del proyecto Structure

📁 BloggingPlatform.Data  
├── 📂 Context          - Contexto de Entity Framework  
├── 📂 Entities         - Entidades del dominio  
├── 📂 Migrations       - Migraciones de la base de datos  
└── 📂 Repositories     - Repositorios de acceso a datos

📁 BloggingPlatform.Server  
├── 📂 Controllers      - Controladores con endpoints REST  
└── 📂 Mapper           - Configuraciones de AutoMapper  
└── 📂 Services         - Lógica de negocio encapsulada en servicios  

📁 BloggingPlatform.Shared  
├── 📂 Todo         - DTOs para elementos de tareas
└── 📂 User         - DTOs para elementos de usuario.
