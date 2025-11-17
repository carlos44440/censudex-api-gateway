using System.ComponentModel.DataAnnotations;

namespace api_gateway.Src.DTOs.Order
{
    /// <summary>
    /// Solicitud para actualizar el estado de un pedido.
    /// </summary>
    public class UpdateOrderStatusDto
    {
        /// <summary>
        /// Estado del pedido.
        /// </summary>
        [Required]
        [RegularExpression("^(?i)(pendiente|en procesamiento|enviado|entregado|cancelado)$", ErrorMessage =
        "El estado del pedido debe ser alguno de estos valores 'pendiente', 'en procesamiento', 'enviado', 'entregado' y 'cancelado'")]
        public string Status { get; set; } = string.Empty;
    }
}