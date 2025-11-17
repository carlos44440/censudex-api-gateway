using System.ComponentModel.DataAnnotations;

namespace api_gateway.Src.DTOs.Order
{
    /// <summary>
    /// Peticion para cancelar un pedido.
    /// </summary>
    public class CancelOrderDto
    {
        /// <summary>
        /// Razon de cancelacion del pedido.
        /// </summary>
        [MinLength(3, ErrorMessage = "La razon de cancelación debe contener al menos 3 letras.")]
        public string? CancellationReason { get; set; }
    }
}