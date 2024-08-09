# IFX Entidades API

Este proyecto proporciona un servicio web para realizar operaciones de consulta, inserción, actualización y eliminación sobre las entidades `Entidad` y `Empleado`.

## Arquitectura

El servicio web está creado utilizando la arquitectura orientada al dominio (DDD) y aplica el patrón de repositorio genérico en la capa de infraestructura para abstraer la responsabilidad del manejo de la base de datos.

El proyecto utiliza SQLite como contexto de base de datos y Entity Framework Core (EF Core) como ORM.

Además, se ha implementado la funcionalidad de login en el Servicio de Autenticación (`AuthService`). Para controlar el acceso a los endpoints expuestos en los controladores, se utiliza JWT (JSON Web Tokens), asegurando que solo los administradores puedan realizar inserciones, actualizaciones o eliminaciones.

## Pasos para ejecutar el proyecto

1. **Clonar el proyecto**

   Usa el siguiente comando para clonar el repositorio:

   ```bash
   git clone https://github.com/SamuelSerrano/IFX_Entidades.API.git

2. **Correr las migraciones iniciales configuradas en el proyecto**

   Ejecuta el siguiente comando para agregar las migraciones iniciales:

   ```bash
   dotnet ef migrations add Init --project IFX_Entidades.API
   
3. **Ejecutar la Base de Datos**

   Actualiza la base de datos con las migraciones aplicadas:

   ```bash
  dotnet ef database update --project IFX_Entidades.API

4. **Ejecutar la solución**

   Ejecuta la aplicación con el siguiente comando:

   ```bash
  dotnet run --project IFX_Entidades.API


## Nota

En la migración se precargan unos usuarios para poder utilizarlos en la creación de los Tokens. No se ha implementado un CRUD para usuarios porque estaba fuera del alcance inicial del proyecto. 

Los usuarios precargados son:

- **Login:** Usuario1
  - **Password:** ClaveFacil
  - **Rol:** Usuario

- **Login:** Admin1
  - **Password:** ClaveAdminFacil
  - **Rol:** Admin
