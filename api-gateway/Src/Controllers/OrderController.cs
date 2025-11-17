using api_gateway.Src.DTOs.Order;
using api_gateway.Src.Helpers;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService;

namespace api_gateway.Src.Controllers
{
    /// <summary>
    /// Controlador para las llamadas al servicio grpc de order.
    /// </summary>
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Cliente grpc para la comunicacion con el servicio de order.
        /// </summary>
        private readonly Order.OrderClient _orderGrpcClient;
        /// <summary>
        /// Servicio para extraer los datos del usuario del JWT.
        /// </summary>
        private readonly UserMetadataExtractor _userMetadataExtractor;

        /// <summary>
        /// Instancia del controladro de order.
        /// </summary>
        /// <param name="orderGrpcClient">Cliente grpc del servicio de order.</param>
        /// <param name="userMetadataExtractor">Servicio extractor de datos del usuario.</param>
        public OrderController(Order.OrderClient orderGrpcClient, UserMetadataExtractor userMetadataExtractor)
        {
            _orderGrpcClient = orderGrpcClient;
            _userMetadataExtractor = userMetadataExtractor;
        }

        /// <summary>
        /// Endpoint para crear un pedido.
        /// </summary>
        /// <param name="createOrder">Peticion con los datos para crear un pedido.</param>
        /// <returns>Retorna 200 ok con el pedido creado en caso de exito o el error correspondiente en caso de fallo.</returns>
        [HttpPost("createOrder")]
        [Authorize(Roles = "CLIENT,ADMIN")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrder)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                //Extraer los datos del usuario.
                // var metadata = _userMetadataExtractor.Extract(User);
                var metadata = new Metadata
                {
                    { "x-user-id", "019a8ac6-79d4-7f5d-a537-bf2ec6d0ff7c" },
                    { "x-user-name", "Carlos Arauco Colque" },
                    { "x-user-role", "CLIENT" },
                    { "x-user-email", "carlos5132fc@gmail.com" }
                };
                
                var createOrderRequest = new CreateOrderRequest{};

                foreach (var item in createOrder.Items)
                {
                    createOrderRequest.Items.Add(new CreateOrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }

                var response = await _orderGrpcClient.CreateOrderAsync(createOrderRequest, metadata);

                return Ok(response);

            } catch (RpcException ex){
                var httpStatus = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(httpStatus, new { message = ex.Status.Detail});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message});
            }
        }

        /// <summary>
        /// Endpoint para consultar el estado de un pedido.
        /// </summary>
        /// <param name="orderId">Id del pedido por consultar.</param>
        /// <returns>Retorna 200 ok con el estado del pedido en caso de exito o el error que corresponda en caso de fallo.</returns>
        [HttpGet("checkOrderStatus/{orderId}")]
        [Authorize(Roles = "CLIENT,ADMIN")]
        public async Task<IActionResult> CheckOrderStatusAsync(string orderId)
        {
            try
            {
                // Extraer los datos del usuario desde el JWT.
                // var metadata = _userMetadataExtractor.Extract(User);
                var metadata = new Metadata
                {
                    { "x-user-id", "019a8ac6-79d4-7f5d-a537-bf2ec6d0ff7c" },
                    { "x-user-name", "Carlos Arauco Colque" },
                    { "x-user-role", "CLIENT" },
                    { "x-user-email", "carlos5132fc@gmail.com" }
                };

                var checkOrderStatusRequest = new CheckOrderStatusRequest
                {
                    OrderId = orderId
                };
                var result = await _orderGrpcClient.CheckOrderStatusAsync(checkOrderStatusRequest, metadata);

                return Ok(result.Status);

            } catch(RpcException ex)
            {
                var httpStatus = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(httpStatus, new { message = ex.Status.Detail});
            } catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualizar el estado de un pedido.
        /// </summary>
        /// <param name="orderId">Id del pedido a actualizar.</param>
        /// <param name="updateOrderStatus">Peticion con los datos necesarios para la actualizacion.</param>
        /// <returns>Retorna 200 ok con el pedido actualizado en caso de exito o el error que corresponda en caso de fallo.</returns>
        [HttpPut("updateOrderStatus/{orderId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateOrderStatusAsync(string orderId, [FromBody] UpdateOrderStatusDto updateOrderStatus)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updateOrderStatusRequest = new UpdateOrderStatusRequest
                {
                    OrderId = orderId,
                    Status = updateOrderStatus.Status
                };
                var response = await _orderGrpcClient.UpdateOrderStatusAsync(updateOrderStatusRequest);

                return Ok(response);
            }
            catch (RpcException ex)
            {
                var httpStatus = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(httpStatus, new { message = ex.Status.Detail});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message});
            }
        }

        /// <summary>
        /// Cancelar un pedido.
        /// </summary>
        /// <param name="orderId">Id del pedido a cancelar.</param>
        /// <param name="cancelOrderDto">Peticion con los datos necesarios para cancelar un pedido.</param>
        /// <returns>Retorna 200 ok con el pedido actualizado en caso de exito o el error que corresponda en caso de fallo.</returns>
        [HttpPut("cancelOrder/{orderId}")]
        [Authorize(Roles = "CLIENT,ADMIN")]
        public async Task<IActionResult> CancelOrderAsync(string orderId, [FromBody] CancelOrderDto cancelOrderDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // var metadata = _userMetadataExtractor.Extract(User);
                var metadata = new Metadata
                {
                    { "x-user-id", "019a8ac6-79d4-7f5d-a537-bf2ec6d0ff7c" },
                    { "x-user-name", "Carlos" },
                    { "x-user-role", "CLIENT" },
                    { "x-user-email", "carlos5132fc@gmail.com" }
                };

                var requestCancelOrder = new RequestCancelOrder
                {
                    OrderId = orderId,
                    CancellationReason = cancelOrderDto.CancellationReason
                };

                var cancelOrderRequest = new CancelOrderRequest
                {
                    RequestCancelOrder = requestCancelOrder
                };

                var response = await _orderGrpcClient.CancelOrderAsync(cancelOrderRequest, metadata);

                return Ok(response);
            }
            catch (RpcException ex)
            {
                var httpStatus = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(httpStatus, new { message = ex.Status.Detail});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener los pedidos.
        /// </summary>
        /// <param name="queryObject">Filtros para los pedidos.</param>
        /// <returns>Retorna 200 ok con los pedidos filtrados en caso de exito o el error que corresponda en caso de fallo.</returns>
        [HttpGet("getOrders")]
        [Authorize(Roles = "CLIENT,ADMIN")]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] QueryObject queryObject)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // var metadata = _userMetadataExtractor.Extract(User);
                var metadata = new Metadata
                {
                    { "x-user-id", "08975dbf-39a8-4d96-8c24-993057eef645" },
                    { "x-user-name", "Carlos" },
                    { "x-user-role", "CLIENT" },
                    { "x-user-email", "carlos5132fc@gmail.com" }
                };

                var queryObjectOrder = new QueryObjectOrder
                {
                    OrderId = queryObject.OrderId ?? "",
                    CustomerId = queryObject.CustomerId ?? "",
                    InitialOrderDate = queryObject.InitialOrderDate?.ToString("O") ?? "",
                    FinalOrderDate = queryObject.FinalOrderDate?.ToString("O") ?? ""
                };

                var getOrdersRequest = new GetOrdersRequest
                {
                    QueryObject = queryObjectOrder
                };
                var response = await _orderGrpcClient.GetOrdersAsync(getOrdersRequest, metadata);
                return Ok(response);

            } catch (RpcException ex)
            {
                var httpStatus = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(httpStatus, new { message = ex.Status.Detail});
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}