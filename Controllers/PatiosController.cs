using Microsoft.AspNetCore.Mvc;
using patioAPI.Models;
using patioAPI.Services;

namespace patioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatiosController : ControllerBase
    {
        private readonly PatioService _service;
        public PatiosController(PatioService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os pátios com paginação.
        /// </summary>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Quantidade de itens por página.</param>
        /// <returns>Lista paginada de pátios.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Patio>), 200)]
        public async Task<ActionResult<IEnumerable<Patio>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var all = await _service.GetAllAsync();
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(new { total = all.Count, page, pageSize, data = paged });
        }

        /// <summary>
        /// Busca um pátio pelo ID.
        /// </summary>
        /// <param name="courtId">ID do pátio.</param>
        /// <returns>Dados do pátio encontrado.</returns>
        [HttpGet("{courtId}")]
        [ProducesResponseType(typeof(Patio), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Patio>> GetById(int courtId)
        {
            var patio = await _service.GetByIdAsync(courtId);
            if (patio == null) return NotFound();
            return Ok(patio);
        }

        /// <summary>
        /// Cadastra um novo pátio.
        /// </summary>
        /// <param name="patio">Dados do pátio.</param>
        /// <returns>Pátio cadastrado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Patio), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Patio>> Create(Patio patio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(patio);
            return CreatedAtAction(nameof(GetById), new { courtId = created.CourtId }, created);
        }

        /// <summary>
        /// Atualiza um pátio.
        /// </summary>
        /// <param name="courtId">ID do pátio.</param>
        /// <param name="patio">Dados atualizados do pátio.</param>
        [HttpPut("{courtId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int courtId, Patio patio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(courtId, patio);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Atualiza parcialmente um pátio.
        /// </summary>
        /// <param name="courtId">ID do pátio.</param>
        /// <param name="updates">Campos a serem atualizados.</param>
        [HttpPatch("{courtId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int courtId, [FromBody] Dictionary<string, object> updates)
        {
            var updated = await _service.PatchAsync(courtId, updates);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um pátio.
        /// </summary>
        /// <param name="courtId">ID do pátio.</param>
        [HttpDelete("{courtId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int courtId)
        {
            var deleted = await _service.DeleteAsync(courtId);
            if (!deleted) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Atualiza área, capacidade e grid do pátio.
        /// </summary>
        /// <param name="courtId">ID do pátio.</param>
        /// <param name="update">Dados de área, capacidade e grid.</param>
        [HttpPut("{courtId}/area-grid")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAreaGrid(int courtId, [FromBody] Patio update)
        {
            var patio = await _service.GetByIdAsync(courtId);
            if (patio == null) return NotFound();
            patio.AreaTotal = update.AreaTotal;
            patio.MaxMotos = update.MaxMotos;
            patio.GridRows = update.GridRows;
            patio.GridCols = update.GridCols;
            await _service.UpdateAsync(courtId, patio);
            return NoContent();
        }

        /// <summary>
        /// Retorna a ocupação do grid do pátio.
        /// </summary>
        /// <param name="courtId">ID do pátio.</param>
        /// <returns>Informações do pátio e ocupação do grid.</returns>
        [HttpGet("{courtId}/ocupacao")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOcupacao(int courtId)
        {
            var patio = await _service.GetByIdAsync(courtId);
            if (patio == null) return NotFound();
            // Busca todas as posições ocupadas no grid desse pátio
            var ocupacao = await _service.GetOcupacaoGridAsync(courtId);
            return Ok(new {
                patio = new { patio.CourtId, patio.CourtLocal, patio.AreaTotal, patio.MaxMotos, patio.GridRows, patio.GridCols },
                ocupacao
            });
        }
    }
}
