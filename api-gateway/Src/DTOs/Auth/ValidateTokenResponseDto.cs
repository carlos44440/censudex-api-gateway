namespace api_gateway.Src.DTOs.Auth
{
    /// <summary>
    /// DTO para la respuesta de validación de token.
    /// </summary>
    public class ValidateTokenResponseDto
    {
        /// <summary>
        /// Indica si el token es válido.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Identificador del usuario asociado al token.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Rol del usuario asociado al token.
        /// </summary>
        public string? Role { get; set; }

        /// <summary>
        /// Mensaje adicional sobre la validación del token.
        /// </summary>
        public string? Message { get; set; }
    }
}
