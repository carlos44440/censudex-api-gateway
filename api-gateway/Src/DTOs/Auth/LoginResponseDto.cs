namespace api_gateway.Src.DTOs.Auth
{
    /// <summary>
    /// DTO para la respuesta de inicio de sesión.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Token de autenticación JWT.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de expiración del token.
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Rol del usuario.
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}
