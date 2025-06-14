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

### usuario por defecto
usuario: admin@portafolio.com
contraseña: 1234

## Administración (Front)

### Dependencias
- daisyUI
- Tailwindcss
- axios
- pinya
- tiptap
- heroicons

### instalación tailwind
https://dev.to/osalumense/install-tailwind-css-v4-in-a-vue-3-vite-project-319g
### instalación daisyui
https://daisyui.com/docs/install/vue/

### .env
Configuración de las variables de entorno
```json
VITE_API_URL=https://localhost:7161
```

### mejoras
- dividir las responsabilidades de los componentes
- mejorar los mensajes de feedback a los usuarios