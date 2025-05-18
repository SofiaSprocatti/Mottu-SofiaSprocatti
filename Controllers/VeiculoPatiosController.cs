using Microsoft.AspNetCore.Mvc;
using patioAPI.Models;
using patioAPI.Services;

namespace patioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoPatiosController : ControllerBase
    {
        private readonly VeiculoPatioService _service;
        public VeiculoPatiosController(VeiculoPatioService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os registros de veículos no pátio, com suporte a paginação.
        /// </summary>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Quantidade de itens por página.</param>
        /// <returns>Lista paginada de veículos no pátio.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VeiculoPatio>), 200)]
        public async Task<ActionResult<IEnumerable<VeiculoPatio>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var all = await _service.GetAllAsync();
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(new { total = all.Count, page, pageSize, data = paged });
        }

        /// <summary>
        /// Retorna alertas de proximidade de veículos no pátio, informando veículos próximos a uma posição.
        /// </summary>
        /// <param name="courtId">ID do pátio.</param>
        /// <param name="x">Coordenada X.</param>
        /// <param name="y">Coordenada Y.</param>
        /// <param name="distancia">Distância máxima para considerar proximidade.</param>
        /// <returns>Lista de alertas de proximidade.</returns>
        [HttpGet("alerta-proximidade")]
        [ProducesResponseType(typeof(List<AlertaProximidadeDto>), 200)]
        public async Task<ActionResult<List<AlertaProximidadeDto>>> GetAlertasProximidade([FromQuery] int courtId, [FromQuery] int x, [FromQuery] int y, [FromQuery] int distancia = 1)
        {
            var alertas = await _service.GetProximidadeAsync(courtId, x, y, distancia);
            return Ok(alertas);
        }

        /// <summary>
        /// Busca um registro específico de veículo no pátio.
        /// </summary>
        /// <param name="vehicleId">ID do veículo.</param>
        /// <param name="courtId">ID do pátio.</param>
        /// <param name="branchId">ID da filial.</param>
        /// <returns>Registro do veículo no pátio.</returns>
        [HttpGet("{vehicleId}/{courtId}/{branchId}")]
        [ProducesResponseType(typeof(VeiculoPatio), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VeiculoPatio>> GetByIds(int vehicleId, int courtId, int branchId)
        {
            var result = await _service.GetByIdsAsync(vehicleId, courtId, branchId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Cria um novo registro de veículo no pátio.
        /// </summary>
        /// <param name="veiculoPatio">Dados do veículo no pátio.</param>
        /// <returns>Registro criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(VeiculoPatio), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<VeiculoPatio>> Create(VeiculoPatio veiculoPatio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(veiculoPatio);
            return CreatedAtAction(nameof(GetByIds), new { vehicleId = created.VehicleId, courtId = created.CourtId, branchId = created.BranchId }, created);
        }

        /// <summary>
        /// Atualiza completamente um registro de veículo no pátio.
        /// </summary>
        /// <param name="vehicleId">ID do veículo.</param>
        /// <param name="courtId">ID do pátio.</param>
        /// <param name="branchId">ID da filial.</param>
        /// <param name="veiculoPatio">Dados atualizados do veículo no pátio.</param>
        [HttpPut("{vehicleId}/{courtId}/{branchId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int vehicleId, int courtId, int branchId, VeiculoPatio veiculoPatio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(vehicleId, courtId, branchId, veiculoPatio);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Atualiza parcialmente um registro de veículo no pátio.
        /// </summary>
        /// <param name="vehicleId">ID do veículo.</param>
        /// <param name="courtId">ID do pátio.</param>
        /// <param name="branchId">ID da filial.</param>
        /// <param name="updates">Campos a serem atualizados.</param>
        [HttpPatch("{vehicleId}/{courtId}/{branchId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int vehicleId, int courtId, int branchId, [FromBody] Dictionary<string, object> updates)
        {
            var updated = await _service.PatchAsync(vehicleId, courtId, branchId, updates);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um registro de veículo do pátio.
        /// </summary>
        /// <param name="vehicleId">ID do veículo.</param>
        /// <param name="courtId">ID do pátio.</param>
        /// <param name="branchId">ID da filial.</param>
        [HttpDelete("{vehicleId}/{courtId}/{branchId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int vehicleId, int courtId, int branchId)
        {
            var deleted = await _service.DeleteAsync(vehicleId, courtId, branchId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

