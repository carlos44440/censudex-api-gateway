using System.ComponentModel.DataAnnotations;

namespace api_gateway.Src.DTOs.Order
{
    /// <summary>
    /// Solicitud para crear un pedido.
    /// </summary>
    public class CreateOrderDto
    {
        /// <summary>
        /// Lista de artículos del pedido.
        /// </summary>
        [Required]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}