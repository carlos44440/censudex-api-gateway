using System.ComponentModel.DataAnnotations;

namespace api_gateway.Src.DTOs.Auth
{
    /// <summary>
    /// DTO para la solicitud de inicio de sesión.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Identificador del usuario (puede ser nombre de usuario o correo electrónico).
        /// </summary>
        [Required]
        public string UsernameOrEmail { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
