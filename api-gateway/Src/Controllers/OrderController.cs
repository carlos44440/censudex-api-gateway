using api_gateway.Src.DTOs.Order;
using api_gateway.Src.Helpers;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService;

namespace api_gateway.Src.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly Order.OrderClient _orderGrpcClient;
        private readonly UserMetadataExtractor _userMetadataExtractor;

        public OrderController(Order.OrderClient orderGrpcClient, UserMetadataExtractor userMetadataExtractor)
        {
            _orderGrpcClient = orderGrpcClient;
            _userMetadataExtractor = userMetadataExtractor;
        }

        [HttpPost("createOrder")]
        // [Authorize(Roles = "CLIENT,ADMIN")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto createOrder)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
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
                return StatusCode((int)ex.StatusCode, new { message = ex.Status.Detail});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message});
            }
        }

        [HttpGet("checkOrderStatus/{orderId}")]
        // [Authorize(Roles = "CLIENT,ADMIN")]
        public async Task<IActionResult> CheckOrderStatusAsync(string orderId)
        {
            try
            {
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
                return StatusCode((int)ex.StatusCode, new { message = ex.Status.Detail });
            } catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("updateOrderStatus/{orderId}")]
        // [Authorize(Roles = "ADMIN")]
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
                return StatusCode((int)ex.StatusCode, new { message = ex.Status.Detail });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message});
            }
        }

        [HttpPut("cancelOrder/{orderId}")]
        // [Authorize(Roles = "CLIENT,ADMIN")]
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
                return StatusCode((int)ex.StatusCode, new { message = ex.Status.Detail });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("getOrders")]
        // [Authorize(Roles = "CLIENT,ADMIN")]
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
                    InitialOrderDate = queryObject.InitialOrderDate?.ToString() ?? "",
                    FinalOrderDate = queryObject.FinalOrderDate?.ToString() ?? ""
                };

                var getOrdersRequest = new GetOrdersRequest
                {
                    QueryObject = queryObjectOrder
                };
                var response = await _orderGrpcClient.GetOrdersAsync(getOrdersRequest, metadata);
                return Ok(response);

            } catch (RpcException ex)
            {
                return StatusCode((int)ex.StatusCode, new { message = ex.Status.Detail });
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}