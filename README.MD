# docu temporal (Sin organizar)
documentación temporal 

## backend

## requisitos
- .net8
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.AspNetCore.Authentication.JwtBearer

## Creación del usuario con permisos para crear el schema en la base de datos.

Para nuestra aplicación, utilizaremos las siguientes credenciales:

Usuario: user_portafolio  
Contraseña: Pass.1234  


```sql
CREATE LOGIN user_portafolio WITH PASSWORD = 'Pass.1234';
ALTER SERVER ROLE dbcreator ADD MEMBER user_portafolio;
```
- Comprobamos que nuestro usuario tiene permisos para loguearse:

```sql
SELECT name, type_desc, is_disabled FROM sys.server_principals WHERE type_desc = 'SQL_LOGIN';
```

Por tratarse de un proyecto didáctico las credenciales quedarán en el connectionString, de lo contrario es aconsejable utilizar archivos .env, vault de azure, aws key management para administrar las credenciales.

```json
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=PORTAFOLIO;TrustServerCertificate=True; User Id=user_portafolio;Password=Pass.1234;"
  },
```

### paquetes nugget instalados

### migraciones

```bash
add-migration Inicial -OutputDir "Contexto/Migraciones"
```
```bash
update-database
```
