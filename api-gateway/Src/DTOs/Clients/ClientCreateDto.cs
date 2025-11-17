using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Src.DTOs.Clients
{
    public class ClientCreateDto
    {
        /// <summary>
        /// Nombre completo del cliente.
        /// </summary>
        [Required]
        public required string FullName { get; set; }

        /// <summary>
        /// Correo electrónico del cliente.
        /// </summary>
        [Required]
        public required string Email { get; set; }

        /// <summary>
        /// Nombre de usuario del cliente.
        /// </summary>
        [Required]
        public required string Username { get; set; }

        /// <summary>
        /// Fecha de nacimiento del cliente.
        /// </summary>
        [Required]
        public required string BirthDate { get; set; }

        /// <summary>
        /// Dirección del cliente.
        /// </summary>
        [Required]
        public required string Address { get; set; }

        /// <summary>
        /// Número de teléfono del cliente.
        /// </summary>
        [Required]
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Contraseña del cliente.
        /// </summary>
        [Required]
        public required string Password { get; set; }
    }
}
