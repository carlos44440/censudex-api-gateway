using System.ComponentModel.DataAnnotations;

namespace api_gateway.Src.DTOs.Order
{
    public class CancelOrderDto
    {
        [MinLength(3, ErrorMessage = "La razon de cancelación debe contener al menos 3 letras.")]
        public string? CancellationReason { get; set; }
    }
}