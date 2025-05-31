# Tablas
## UsuarioAdmin
Autenticación del panel (JWT)

| Campo        | Descripción                 |
| ------------ | --------------------------- |
| Id           | PK, Identity                |
| Email        | Único, no nulo              |
| PasswordHash | Hash de contraseña, no nulo |
| CreatedAt    | Fecha creación              |
| UpdatedAt    | Fecha última modificación   |

## Perfil
Datos personales básicos y bio

| Campo          | Descripción                     |
| -------------- | ------------------------------- |
| Id             | PK, Identity                    |
| UsuarioAdminId | FK → UsuarioAdmin(Id), no nulo  |
| Nombre         | Nombre(s), no nulo              |
| Apellidos      | Apellidos, no nulo              |
| Saludo         | Saludo de la primera sección |
| Descripcion    | Descripción breve de la primera sección|
| AcercaDeMi        | Descripción extendida. Sección 2 |
| FotoURL       | URL foto de perfil, opcional    |
| CreatedAt      | Fecha creación                  |
| UpdatedAt      | Fecha última modificación       |

## Habilidad
Lista de habilidades técnicas

| Campo     | Descripción                               |
| --------- | ----------------------------------------- |
| Id        | PK, Identity                              |
| Nombre    | Nombre de la habilidad, no nulo           |
| LogoUrl   | URL de logo, opcional                     |
| EsActual  | Booleano: true = está vigente, false = no |
| CreatedAt | Fecha creación                            |
| UpdatedAt | Fecha última modificación                 |

## Proyecto
Tarjetas de proyectos con orden y versiones de imagen
| Campo            | Descripción                                         |
| ---------------- | --------------------------------------------------- |
| Id               | PK, Identity                                        |
| Titulo           | Título del proyecto, no nulo                        |
| Descripcion      | Descripción detallada, no nulo                      |
| ImagenDesktopUrl | URL de imagen para escritorio, opcional             |
| ImagenMobileUrl  | URL de imagen para móvil, opcional                  |
| RepoUrl          | Enlace al repositorio, opcional                     |
| LiveUrl          | Enlace al sitio desplegado, opcional                |
| Orden            | Entero para ordenar/resaltar, no nulo (default = 0) |
| CreatedAt        | Fecha creación                                      |
| UpdatedAt        | Fecha última modificación                           |

## Conocimiento
Frameworks, patrones y arquitecturas usados

| Campo     | Descripción                      |
| --------- | -------------------------------- |
| Id        | PK, Identity                     |
| Nombre    | Nombre del conocimiento, no nulo |
| CreatedAt | Fecha creación                   |
| UpdatedAt | Fecha última modificación        |

## ProyectoHabilidad (N:M)
Vincula proyectos con habilidades

| Campo       | Descripción        |
| ----------- | ------------------ |
| ProyectoId  | FK → Proyecto(Id)  |
| HabilidadId | FK → Habilidad(Id) |

## ProyectoConocimiento (N:M)
Vincula proyectos con conocimientos
| Campo          | Descripción           |
| -------------- | --------------------- |
| ProyectoId     | FK → Proyecto(Id)     |
| ConocimientoId | FK → Conocimiento(Id) |

## Educación
Formación académica
| Campo       | Descripción                       |
| ----------- | --------------------------------- |
| Id          | PK, Identity                      |
| PerfilId    | FK → Perfil(Id), no nulo          |
| Institucion | Nombre de la institución, no nulo |
| Titulo      | Título obtenido, no nulo          |
| FechaInicio | Fecha de inicio, no nulo          |
| FechaFin    | Fecha de fin, opcional (en curso) |
| Descripcion | Detalles adicionales, opcional    |

## Empleo
Experiencia laboral

| Campo       | Descripción                       |
| ----------- | --------------------------------- |
| Id          | PK, Identity                      |
| PerfilId    | FK → Perfil(Id), no nulo          |
| Empresa     | Nombre de la empresa, no nulo     |
| Cargo       | Puesto desempeñado, no nulo       |
| FechaInicio | Fecha de inicio, no nulo          |
| FechaFin    | Fecha de fin, opcional (en curso) |
| Descripcion | Detalles adicionales, opcional    |

## RedSocialContacto
Redes sociales y canales de contacto
| Campo      | Descripción                                  |
| ---------- | -------------------------------------------- |
| Id         | PK, Identity                                 |
| PerfilId   | FK → Perfil(Id), no nulo                     |
| Plataforma | Nombre de la red (LinkedIn, GitHub), no nulo |
| Url        | Enlace a perfil, no nulo                     |
| IconUrl    | URL de icono/logo, opcional                  |
