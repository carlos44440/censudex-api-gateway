using api_gateway.Src.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace api_gateway.Src.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones relacionadas con la autenticación.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IHttpClientFactory httpClientFactory) : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        /// <summary>
        /// Maneja la solicitud de inicio de sesión.
        /// </summary>
        /// <param name="loginDto">DTO que contiene la información de inicio de sesión.</param>
        /// <returns>Respuesta HTTP con el resultado del intento de inicio de sesión.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = _httpClientFactory.CreateClient("AuthService");

            var response = await client.PostAsJsonAsync("/api/auth/login", loginDto);

            var content = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            return StatusCode((int)response.StatusCode, content);
        }

        /// <summary>
        /// Valida el token de autenticación proporcionado en la cabecera Authorization.
        /// </summary>
        /// <returns>Respuesta HTTP con el resultado de la validación del token.</returns>
        [HttpGet("validate-token")]
        public async Task<IActionResult> ValidateToken()
        {
            var token = Request.Headers.Authorization.FirstOrDefault();
            if (token == null)
                return Unauthorized(new { Message = "Token faltante." });

            var client = _httpClientFactory.CreateClient("AuthService");
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.GetAsync("/api/auth/validate-token");

            var content = await response.Content.ReadFromJsonAsync<ValidateTokenResponseDto>();

            return StatusCode((int)response.StatusCode, content);
        }

        /// <summary>
        /// Cierra la sesión del usuario invalidando el token de autenticación.
        /// </summary>
        /// <returns>Respuesta HTTP con el resultado del intento de cierre de sesión.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers.Authorization.FirstOrDefault();
            if (token == null)
                return Unauthorized(new { Message = "Token faltante." });

            var client = _httpClientFactory.CreateClient("AuthService");
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.PostAsync("/api/auth/logout", null);

            var content = await response.Content.ReadFromJsonAsync<LogoutResponseDto>();

            return StatusCode((int)response.StatusCode, content);
        }
    }
}
