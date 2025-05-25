namespace portafolio.backend.API.Utilidades
{
    public class PasswordHelper
    {

        public static bool ComprobarPassword(string password, string hashedPassword)
        {
           
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
