


# Historia de Usuario 2
## Guía para seguir como ejemplo. No necesariamente se apega al 100% del contenid.
**“Como desarrollador del sistema, quiero crear endpoints en la API para servir la información del portfolio, para permitir que el frontend consuma datos dinámicamente sin modificar el código fuente.”** 

---

## 1. Criterios de aceptación  
1. Existen **endpoints GET** para todas las secciones principales:  
   - Perfil  
   - Habilidades  
   - Proyectos  
   - Formación (Educación + Empleo)  
   - Conocimientos adicionales  
   - Enlaces a redes sociales  
2. Los endpoints devuelven datos en **formato JSON** con estructura consistente (DTOs).  
3. Las respuestas incluyen **códigos HTTP apropiados** (200 si hay datos, 404 si no existe, 500 en error interno).  
4. Cuando corresponda (listados potencialmente extensos), la información se devuelve **paginada** (por ejemplo, en `/api/proyectos`).  

---

## 2. Flujo general de cada endpoint “GET” (secuencia paso a paso)  

Para ilustrar el flujo, tomaremos el ejemplo del endpoint “Obtener todos los proyectos” (`GET /api/proyectos`):

1. **Cliente (Frontend) envía**  
   - GET `/api/proyectos?page=1&pageSize=10`  
   - (Parámetros opcionales: `page`, `pageSize` para paginación)  

2. **ProyectoController recibe la petición**  
   - Obtiene los parámetros de query (`page`, `pageSize`).  
   - Llama a `ProyectoService.ObtenerProyectosAsync(page, pageSize)`.  

3. **ProyectoService ejecuta lógica de negocio**  
   - Llama a `ProyectoRepository.GetPagedAsync(page, pageSize)`.  
   - El repositorio realiza la consulta a la BD con EF Core, aplicando `Skip((page – 1) * pageSize).Take(pageSize)`.  
   - Mapea cada entidad `Proyecto` a un DTO `ProyectoDto`.  
   - Empaqueta la lista en un objeto de paginación, p.ej. `PagedResult<ProyectoDto>`.  

4. **ProyectoService devuelve resultado al controlador**  
   - Si hay al menos un elemento, construye `ApiResponse<PagedResult<ProyectoDto>>` con `Success = true`, `Data = pagedResult`, `Message = ""`, `Errors = []`.  
   - Si no hay ningún proyecto y se está buscando una página fuera de rango, devuelve `null` o un `PagedResult` vacío.  

5. **ProyectoController formatea la respuesta**  
   - Si `pagedResult.Items.Count > 0` →  
     - **200 OK** +  
       ```json
       {
         "exitoso": true,
         "mensaje": "",
         "datos": {},
         "codigoEstado": 200
       }
       ```
   - Si no hay datos en la página solicitada (por ejemplo, `page` > `TotalPages`) →  
     - **404 Not Found** +  
       ```json
       {
         "exitoso": false,
         "mensaje": "",
         "datos": {},
         "codigoEstado": 404
       }
       ```
   - En caso de **error inesperado** (excepción), →  
     - **500 Internal Server Error** +  
       ```json
       {
           "exitoso": false,
         "mensaje": "",
         "datos": {},
         "codigoEstado": 500
       }
       ```

> **Nota**: el mismo flujo aplica a cada recurso (Perfil, Habilidad, Educación, Empleo, Conocimiento, EnlaceSocial), ajustando el repositorio, DTOs y rutas correspondientes.  

---

## 3. Componentes y clases involucradas  

### 3.1. Controladores (Controllers/)  
- **PerfilController**  
  - `GET /api/perfil` → `ObtenerPerfilAsync(): ApiResponse<PerfilDto>`  
- **HabilidadController**  
  - `GET /api/habilidades` → `ObtenerHabilidadesAsync(page, pageSize): ApiResponse<PagedResult<HabilidadDto>>`  
  - `GET /api/habilidades/{id}` → `ObtenerHabilidadPorIdAsync(id): ApiResponse<HabilidadDto>`  
- **ProyectoController**  
  - `GET /api/proyectos` → `ObtenerProyectosAsync(page, pageSize): ApiResponse<PagedResult<ProyectoDto>>`  
  - `GET /api/proyectos/{id}` → `ObtenerProyectoPorIdAsync(id): ApiResponse<ProyectoDto>`  
- **EducacionController**  
  - `GET /api/educacion` → `ObtenerEducacionesAsync(page, pageSize): ApiResponse<PagedResult<EducacionDto>>`  
  - `GET /api/educacion/{id}` → `ObtenerEducacionPorIdAsync(id): ApiResponse<EducacionDto>`  
- **EmpleoController**  
  - `GET /api/empleos` → `ObtenerEmpleosAsync(page, pageSize): ApiResponse<PagedResult<EmpleoDto>>`  
  - `GET /api/empleos/{id}` → `ObtenerEmpleoPorIdAsync(id): ApiResponse<EmpleoDto>`  
- **ConocimientoController**  
  - `GET /api/conocimientos` → `ObtenerConocimientosAsync(page, pageSize): ApiResponse<PagedResult<ConocimientoDto>>`  
  - `GET /api/conocimientos/{id}` → `ObtenerConocimientoPorIdAsync(id): ApiResponse<ConocimientoDto>`  
- **EnlaceSocialController**  
  - `GET /api/enlaces` → `ObtenerEnlacesSocialesAsync(page, pageSize): ApiResponse<PagedResult<EnlaceSocialDto>>`  
  - `GET /api/enlaces/{id}` → `ObtenerEnlaceSocialPorIdAsync(id): ApiResponse<EnlaceSocialDto>`  

> Cada controlador inyecta su `XService` correspondiente (por ejemplo, `PerfilService` en `PerfilController`).

---

### 3.2. Servicios (Services/)  
- **PerfilService**  
  - `Task<PerfilDto?> ObtenerPerfilAsync()`  
- **HabilidadService**  
  - `Task<PagedResult<HabilidadDto>> ObtenerHabilidadesAsync(int page, int pageSize)`  
  - `Task<HabilidadDto?> ObtenerHabilidadPorIdAsync(int id)`  
- **ProyectoService**  
  - `Task<PagedResult<ProyectoDto>> ObtenerProyectosAsync(int page, int pageSize)`  
  - `Task<ProyectoDto?> ObtenerProyectoPorIdAsync(int id)`  
- **EducacionService**  
  - `Task<PagedResult<EducacionDto>> ObtenerEducacionesAsync(int page, int pageSize)`  
  - `Task<EducacionDto?> ObtenerEducacionPorIdAsync(int id)`  
- **EmpleoService**  
  - `Task<PagedResult<EmpleoDto>> ObtenerEmpleosAsync(int page, int pageSize)`  
  - `Task<EmpleoDto?> ObtenerEmpleoPorIdAsync(int id)`  
- **ConocimientoService**  
  - `Task<PagedResult<ConocimientoDto>> ObtenerConocimientosAsync(int page, int pageSize)`  
  - `Task<ConocimientoDto?> ObtenerConocimientoPorIdAsync(int id)`  
- **EnlaceSocialService**  
  - `Task<PagedResult<EnlaceSocialDto>> ObtenerEnlacesSocialesAsync(int page, int pageSize)`  
  - `Task<EnlaceSocialDto?> ObtenerEnlaceSocialPorIdAsync(int id)`  

> Cada servicio orquesta la llamada al repositorio, mapea entidad → DTO y prepara el resultado.

---

### 3.3. Repositorios (AccesoDatos/Repositorios/)  
- **PerfilRepository**  
  - `Task<Perfil> GetPerfilAsync()`  
- **HabilidadRepository**  
  - `Task<PagedResult<Habilidad>> GetPagedHabilidadesAsync(int page, int pageSize)`  
  - `Task<Habilidad> GetHabilidadByIdAsync(int id)`  
- **ProyectoRepository**  
  - `Task<PagedResult<Proyecto>> GetPagedProyectosAsync(int page, int pageSize)`  
  - `Task<Proyecto> GetProyectoByIdAsync(int id)`  
- **EducacionRepository**  
  - `Task<PagedResult<Educacion>> GetPagedEducacionesAsync(int page, int pageSize)`  
  - `Task<Educacion> GetEducacionByIdAsync(int id)`  
- **EmpleoRepository**  
  - `Task<PagedResult<Empleo>> GetPagedEmpleosAsync(int page, int pageSize)`  
  - `Task<Empleo> GetEmpleoByIdAsync(int id)`  
- **ConocimientoRepository**  
  - `Task<PagedResult<Conocimiento>> GetPagedConocimientosAsync(int page, int pageSize)`  
  - `Task<Conocimiento> GetConocimientoByIdAsync(int id)`  
- **EnlaceSocialRepository**  
  - `Task<PagedResult<EnlaceSocial>> GetPagedEnlacesAsync(int page, int pageSize)`  
  - `Task<EnlaceSocial> GetEnlaceByIdAsync(int id)`  

> Todos los métodos usan EF Core (`DbSet<TEntity>.Skip(...).Take(...)`, `FirstOrDefaultAsync`, etc.) y devuelven entidades puras.

---

### 3.4. DTOs (Dominio/DTOs/)  
- **PerfilDto**  
  - `int Id`  
  - `string Nombres`  
  - `string Apellidos`  
  - `string ShortBio`  
  - `string LongBio?`  
  - `string PhotoUrl?`  

- **HabilidadDto**  
  - `int Id`  
  - `string Nombre`  
  - `string LogoUrl?`  
  - `bool EsActual`  

- **ProyectoDto**  
  - `int Id`  
  - `string Titulo`  
  - `string Descripcion`  
  - `string ImagenDesktopUrl?`  
  - `string ImagenMobileUrl?`  
  - `string RepoUrl?`  
  - `string LiveUrl?`  
  - `int Orden`  

- **EducacionDto**  
  - `int Id`  
  - `string Institucion`  
  - `string Titulo`  
  - `DateTime FechaInicio`  
  - `DateTime? FechaFin`  
  - `string? Descripcion`  

- **EmpleoDto**  
  - `int Id`  
  - `string Empresa`  
  - `string Cargo`  
  - `DateTime FechaInicio`  
  - `DateTime? FechaFin`  
  - `string? Descripcion`  

- **ConocimientoDto**  
  - `int Id`  
  - `string Nombre`  

- **EnlaceSocialDto**  
  - `int Id`  
  - `string Plataforma`  
  - `string Url`  
  - `string? IconUrl`  

- **PagedResult<T>** (genérico)  
  ```csharp
  public class PagedResult<T>
  {
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
  }


---

### 3.5. Respuesta estándar (ApiResponse<T>)

```csharp
public class ApiResponse<T>
{
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public T? Data { get; set; }
  public List<string> Errors { get; set; } = new List<string>();
}
```

---

### 3.6. Códigos HTTP

| Código | Uso                                               |
| ------ | ------------------------------------------------- |
| 200    | Datos encontrados (incluso lista vacía)           |
| 404    | Recurso no encontrado (ej. `/api/proyectos/999`)  |
| 400    | Parámetros inválidos (p. ej. `pageSize` negativo) |
| 500    | Error interno inesperado                          |

---

## 4. Tareas de implementación

1. **Definir DTOs**

   * Crear clases en `Dominio/DTOs/` para cada recurso y `PagedResult<T>`.

2. **Configurar el DbContext**

   * Verificar que las **entidades** (`Perfil`, `Habilidad`, `Proyecto`, `Educacion`, `Empleo`, `Conocimiento`, `EnlaceSocial`) están correctamente mapeadas en `Datos/ApplicationDbContext.cs`.

3. **Implementar repositorios**

   * `PerfilRepository.GetPerfilAsync()`
   * `HabilidadRepository.GetPagedHabilidadesAsync(page, pageSize)` y `GetHabilidadByIdAsync(id)`
   * `ProyectoRepository.GetPagedProyectosAsync(page, pageSize)` y `GetProyectoByIdAsync(id)`
   * `EducacionRepository.GetPagedEducacionesAsync(page, pageSize)` y `GetEducacionByIdAsync(id)`
   * `EmpleoRepository.GetPagedEmpleosAsync(page, pageSize)` y `GetEmpleoByIdAsync(id)`
   * `ConocimientoRepository.GetPagedConocimientosAsync(page, pageSize)` y `GetConocimientoByIdAsync(id)`
   * `EnlaceSocialRepository.GetPagedEnlacesAsync(page, pageSize)` y `GetEnlaceByIdAsync(id)`

4. **Implementar servicios**

   * `PerfilService.ObtenerPerfilAsync()` → llama a `PerfilRepository` y mapea a `PerfilDto`.
   * `HabilidadService` → mapea a `HabilidadDto` y construye `PagedResult<HabilidadDto>`.
   * `ProyectoService` → mapea a `ProyectoDto` y construye `PagedResult<ProyectoDto>`.
   * `EducacionService`, `EmpleoService`, `ConocimientoService`, `EnlaceSocialService` con lógica similar.

5. **Implementar controladores**

   * Crear rutas y métodos en cada controlador para exponer los endpoints GET.
   * Inyectar el servicio correspondiente vía constructor.
   * Validar parámetros de paginación (`page`, `pageSize`), devolver `ApiResponse<T>`.


---


