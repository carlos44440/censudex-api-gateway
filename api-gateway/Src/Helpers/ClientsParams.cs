using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Src.Helpers
{
    /// <summary>
    /// Parámetros para filtrar y buscar clientes.
    /// </summary>
    public class ClientsParams
    {
        /// <summary>
        /// Filtrar los clientes por su estado activo/inactivo.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Nombre de usuario para búsqueda parcial.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Correo electrónico para búsqueda parcial.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Nombre completo para búsqueda parcial.
        /// </summary>
        public string? FullName { get; set; }
    }
}
