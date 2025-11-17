namespace api_gateway.Src.DTOs.Auth
{
    /// <summary>
    /// DTO para la respuesta de cierre de sesión.
    /// </summary>
    public class LogoutResponseDto
    {
        /// <summary>
        /// Mensaje de respuesta al cerrar sesión.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
