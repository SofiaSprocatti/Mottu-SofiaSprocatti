using Microsoft.AspNetCore.Mvc;
using patioAPI.Models;
using patioAPI.Services;

namespace patioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotosController : ControllerBase
    {
        private readonly MotoService _service;
        public MotosController(MotoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todas as motos com paginação.
        /// </summary>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Quantidade de itens por página.</param>
        /// <returns>Lista paginada de motos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Moto>), 200)]
        public async Task<ActionResult<IEnumerable<Moto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var all = await _service.GetAllAsync();
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(new { total = all.Count, page, pageSize, data = paged });
        }

        /// <summary>
        /// Busca uma moto pelo ID.
        /// </summary>
        /// <param name="vehicleId">ID da moto.</param>
        /// <returns>Dados da moto encontrada.</returns>
        [HttpGet("{vehicleId}")]
        [ProducesResponseType(typeof(Moto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Moto>> GetById(int vehicleId)
        {
            var moto = await _service.GetByIdAsync(vehicleId);
            if (moto == null) return NotFound();
            return Ok(moto);
        }

        /// <summary>
        /// Busca motos por filial (branch).
        /// </summary>
        /// <param name="branch">Nome da filial.</param>
        /// <returns>Lista de motos da filial.</returns>
        [HttpGet("branch/{branch}")]
        [ProducesResponseType(typeof(IEnumerable<Moto>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Moto>>> GetByBranch(string branch)
        {
            var motos = await _service.GetByBranchAsync(branch);
            if (!motos.Any()) return NotFound();
            return Ok(motos);
        }

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        /// <param name="moto">Dados da moto.</param>
        /// <returns>Moto cadastrada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Moto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Moto>> Create(Moto moto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(moto);
            return CreatedAtAction(nameof(GetById), new { vehicleId = created.VehicleId }, created);
        }

        /// <summary>
        /// Atualiza uma moto.
        /// </summary>
        /// <param name="vehicleId">ID da moto.</param>
        /// <param name="moto">Dados atualizados da moto.</param>
        [HttpPut("{vehicleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int vehicleId, Moto moto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(vehicleId, moto);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Atualiza parcialmente uma moto.
        /// </summary>
        /// <param name="vehicleId">ID da moto.</param>
        /// <param name="updates">Campos a serem atualizados.</param>
        [HttpPatch("{vehicleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int vehicleId, [FromBody] Dictionary<string, object> updates)
        {
            var updated = await _service.PatchAsync(vehicleId, updates);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove uma moto.
        /// </summary>
        /// <param name="vehicleId">ID da moto.</param>
        [HttpDelete("{vehicleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int vehicleId)
        {
            var deleted = await _service.DeleteAsync(vehicleId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
