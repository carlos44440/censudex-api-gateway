namespace api_gateway.Src.Helpers
{
    /// <summary>
    /// Objeto de consulta utilizado para aplicar filtros al obtener pedidos.
    /// </summary>
    public class QueryObject
    {
        /// <summary>
        /// Identificador único del pedido.
        /// </summary>
        public string? OrderId { get; set; }

        /// <summary>
        /// Identificador único del cliente asociado al pedido.
        /// </summary>
        public string? CustomerId { get; set; }

        /// <summary>
        /// Fecha inicial para filtrar pedidos según su fecha de creación.
        /// </summary>
        public DateTime? InitialOrderDate { get; set; }

        /// <summary>
        /// Fecha final para filtrar pedidos según su fecha de creación.
        /// </summary>
        public DateTime? FinalOrderDate { get; set; }
    }
}