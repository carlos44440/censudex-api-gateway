using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Src.DTOs.Clients
{
    /// <summary>
    /// DTO para la transferencia de datos del cliente.
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// Identificador único del cliente.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Nombre completo del cliente.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del cliente.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de usuario del cliente.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de nacimiento del cliente.
        /// </summary>
        public string BirthDate { get; set; } = string.Empty;

        /// <summary>
        /// Dirección del cliente.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Número de teléfono del cliente.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Indica si el cliente está activo.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
