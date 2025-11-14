using Grpc.Core;
using System.Security.Claims;

namespace api_gateway.Src.Helpers
{
    /// <summary>
    /// Clase para extraer los datos del usuario desde el JWT.
    /// </summary>
    public class UserMetadataExtractor
    {
        /// <summary>
        /// Recibe el ClaimsPrincipal (User) y devuelve los headers gRPC (Metadata)
        /// </summary>
        public Metadata Extract(ClaimsPrincipal user)
        {
            var userId = user.FindFirst("sub")?.Value;
            var name = user.FindFirst("name")?.Value;
            var role = user.FindFirst("role")?.Value;
            var email = user.FindFirst("email")?.Value;

            if (userId is null || name is null || role is null || email is null)
                throw new UnauthorizedAccessException("Token inválido o incompleto.");

            var metadata = new Metadata
            {
                { "x-user-id", userId },
                { "x-user-name", name },
                { "x-user-role", role },
                { "x-user-email", email }
            };

            return metadata;
        }
    }
}