using api_gateway.Src.DTOs.Clients;
using api_gateway.Src.Helpers;
using ClientsService.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace api_gateway.Src.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones relacionadas con los clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientsGrpc.ClientsGrpcClient clientsGrpcClient) : ControllerBase
    {
        private readonly ClientsGrpc.ClientsGrpcClient _clientsGrpcClient = clientsGrpcClient;

        /// <summary>
        /// Obtiene una lista de clientes según los parámetros de consulta proporcionados.
        /// </summary>
        /// <param name="query">Los parámetros de consulta para filtrar los clientes.</param>
        /// <returns>Retorna una lista de clientes que coinciden con los parámetros de consulta.</returns>
        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] ClientsParams query)
        {
            try
            {
                var request = new GetClientsRequest
                {
                    Username = query.Username ?? "",
                    Email = query.Email ?? "",
                    FullName = query.FullName ?? "",
                };

                if (query.IsActive.HasValue)
                {
                    request.IsActive = query.IsActive;
                }

                var response = await _clientsGrpcClient.GetAllClientsAsync(request);

                return Ok(response.Clients);
            }
            catch (RpcException ex)
            {
                var http = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(http, new { message = ex.Status.Detail });
            }
        }

        /// <summary>
        /// Obtiene un cliente por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único del cliente.</param>
        /// <returns>Retorna el cliente correspondiente al identificador proporcionado.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(string id)
        {
            try
            {
                var response = await _clientsGrpcClient.GetClientByIdAsync(
                    new GetClientByIdRequest { Id = id }
                );

                return Ok(response);
            }
            catch (RpcException ex)
            {
                var http = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(http, new { message = ex.Status.Detail });
            }
        }

        /// <summary>
        /// Crea un nuevo cliente con la información proporcionada.
        /// </summary>
        /// <param name="dto">Los datos del cliente a crear.</param>
        /// <returns>Retorna el cliente creado.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateDto dto)
        {
            try
            {
                var request = new CreateClientRequest
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Username = dto.Username,
                    BirthDate = dto.BirthDate,
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                    Password = dto.Password,
                };

                var response = await _clientsGrpcClient.CreateClientAsync(request);

                return Ok(response);
            }
            catch (RpcException ex)
            {
                var http = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(http, new { message = ex.Status.Detail });
            }
        }

        /// <summary>
        /// Actualiza la información de un cliente existente.
        /// </summary>
        /// <param name="id">Identificador único del cliente.</param>
        /// <param name="dto">Datos actualizados del cliente.</param>
        /// <returns>Retorna el cliente actualizado.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateClient(string id, [FromBody] ClientUpdateDto dto)
        {
            try
            {
                var request = new UpdateClientRequest
                {
                    Id = id,
                    FullName = dto.FullName ?? "",
                    Email = dto.Email ?? "",
                    Username = dto.Username ?? "",
                    BirthDate = dto.BirthDate?.ToString() ?? "",
                    Address = dto.Address ?? "",
                    PhoneNumber = dto.PhoneNumber ?? "",
                    Password = dto.Password ?? "",
                };

                var response = await _clientsGrpcClient.UpdateClientAsync(request);

                return Ok(response);
            }
            catch (RpcException ex)
            {
                var http = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(http, new { message = ex.Status.Detail });
            }
        }

        /// <summary>
        /// Desactiva un cliente por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del cliente.</param>
        /// <returns>Retorna el cliente desactivado.</returns>
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> DeactivateClient(string id)
        {
            try
            {
                var response = await _clientsGrpcClient.DeactivateClientAsync(
                    new DeactivateClientRequest { Id = id }
                );

                return Ok(response);
            }
            catch (RpcException ex)
            {
                var http = GrpcErrorMapper.ToHttpStatusCode(ex.StatusCode);
                return StatusCode(http, new { message = ex.Status.Detail });
            }
        }
    }
}
