
# Historia de Usuario 1  
**“Como administrador, quiero poder autenticarme de forma segura en el sistema, para proteger el acceso a la gestión de contenidos.”**

---

## 1. Criterios de aceptación  
1. **Endpoint de login** que acepte email/usuario y contraseña para validar las credenciales.  
2. **Hash+salt** para almacenamiento de contraseñas, nunca en texto plano.  
3. **Token JWT** devuelto tras autenticación exitosa, con **expiración de 24 horas**.  

---

## 2. Flujo de autenticación (secuencia paso a paso)  
1. **Cliente (Admin) envía**  
   - POST `/auth/login`  
   - Payload: `{ email, password }`  
2. **AuthController recibe y valida DTO**  
   - Comprueba que `email` y `password` no estén vacíos y tengan formato válido.  
   - Si falla validación → **400 Bad Request** (`ApiResponse.Success = false`, lista de errores).  
3. **AuthController → AuthService.Login(requestDto)**  
   - Pasa `LoginRequestDto { Email, Password }`.  
4. **AuthService obtiene usuario**  
   - Llama a `UsuarioAdminRepository.GetByEmail(email)`.  
5. **Repositorio consulta BD**  
   - `SELECT * FROM UsuarioAdmin WHERE Email = ?`  
   - Devuelve `UsuarioAdmin` o `null`.  
6. **AuthService valida credenciales**  
   - Si `null` o hash no coincide → **401 Unauthorized**.  
7. **AuthService genera JWT**  
   - `JwtUtil.GenerateToken(usuario)` → `string token`.  
8. **AuthService devuelve resultado**  
   - Crea `AuthResponseDto { Token, UsuarioId }`.  
9. **AuthController formatea ApiResponse**  
   - **200 OK** con  
     ```json
     {
       "Success": true,
       "Message": "Autenticación exitosa",
       "Data": { "Token": "...", "UsuarioId": 123 },
       "Errors": []
     }
     ```  
   - O **401 Unauthorized** con  
     ```json
     {
       "Success": false,
       "Message": "Usuario o contraseña inválidos",
       "Data": null,
       "Errors": []
     }
     ```

## 3. Componentes y clases  
- **AuthController**  
  - Responsabilidad: exponer `/auth/login`, orquestar llamada a servicio y formatear `ApiResponse<T>`.  
  - Método: `Login(LoginRequestDto): ApiResponse<AuthResponseDto>`  

- **AuthService**  
  - Lógica de negocio: validación de usuario/password, generación de token.  
  - Método: `Login(requestDto): AuthResult`  

- **UsuarioAdminRepository**  
  - Acceso a datos (EF Core).  
  - Método: `GetByEmail(string email): UsuarioAdmin?`  

- **JwtUtil**  
  - Generación/validación de JWT (firma, expiración).  
  - Método: `GenerateToken(UsuarioAdmin): string`  

- **DTOs**  
  - `LoginRequestDto { string Email; string Password; }`  
  - `AuthResponseDto { string Token; int UsuarioId; }`  

- **ApiResponse<T>**  
  ```ts
  interface ApiResponse<T> {
    Success: boolean;
    Message: string;
    Data: T | null;
    Errors: string[];
  }




## 4. Códigos HTTP

| Código | Uso                                |
| ------ | ---------------------------------- |
| 200    | Login exitoso                      |
| 400    | Petición malformada (DTO inválido) |
| 401    | Credenciales incorrectas           |
| 500    | Error interno inesperado           |



## 5. Tareas de implementación

1. **Definir DTOs**

   * `LoginRequestDto`, `AuthResponseDto`
2. **Configurar hash+salt**

   * Inyectar `IPasswordHasher` para almacenar `PasswordHash`.
3. **Implementar repositorio**

   * `UsuarioAdminRepository.GetByEmail(email)`
4. **Desarrollar AuthService.Login**

   * Obtener usuario, validar hash, generar error o token.
5. **Configurar JwtUtil.GenerateToken**

   * Clave secreta, expiración a 24 h.
6. **Crear AuthController**

   * Método POST `/auth/login` → `AuthService`, envolver en `ApiResponse`.

