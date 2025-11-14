using System.ComponentModel.DataAnnotations;

namespace api_gateway.Src.DTOs.Order
{
    /// <summary>
    /// DTO utilizado para representar un ítem dentro
    /// de la solicitud de creación de un pedido.
    /// </summary>
    public class CreateOrderItemDto
    {
        /// <summary>
        /// Identificador único del producto incluido en el pedido.
        /// </summary>
        [Required]
        [MinLength(36, ErrorMessage = "El id del producto debe tener una longitud minima de 36 caracteres.")]
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad solicitada del producto.
        /// Debe ser mayor a 0 según la validación definida.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Quantity { get; set; }
    }
}