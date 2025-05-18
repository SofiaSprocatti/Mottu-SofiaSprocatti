using Microsoft.AspNetCore.Mvc;
using patioAPI.Models;
using patioAPI.Services;

namespace patioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiliaisController : ControllerBase
    {
        private readonly FilialService _service;
        public FiliaisController(FilialService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todas as filiais com paginação.
        /// </summary>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Quantidade de itens por página.</param>
        /// <returns>Lista paginada de filiais.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Filial>), 200)]
        public async Task<ActionResult<IEnumerable<Filial>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var all = await _service.GetAllAsync();
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(new { total = all.Count, page, pageSize, data = paged });
        }

        /// <summary>
        /// Busca uma filial pelo ID.
        /// </summary>
        /// <param name="branchId">ID da filial.</param>
        /// <returns>Dados da filial encontrada.</returns>
        [HttpGet("{branchId}")]
        [ProducesResponseType(typeof(Filial), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Filial>> GetById(int branchId)
        {
            var filial = await _service.GetByIdAsync(branchId);
            if (filial == null) return NotFound();
            return Ok(filial);
        }

        /// <summary>
        /// Cadastra uma nova filial.
        /// </summary>
        /// <param name="filial">Dados da filial.</param>
        /// <returns>Filial cadastrada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Filial), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Filial>> Create(Filial filial)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(filial);
            return CreatedAtAction(nameof(GetById), new { branchId = created.BranchId }, created);
        }

        /// <summary>
        /// Atualiza uma filial.
        /// </summary>
        /// <param name="branchId">ID da filial.</param>
        /// <param name="filial">Dados atualizados da filial.</param>
        [HttpPut("{branchId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int branchId, Filial filial)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(branchId, filial);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Atualiza parcialmente uma filial.
        /// </summary>
        /// <param name="branchId">ID da filial.</param>
        /// <param name="updates">Campos a serem atualizados.</param>
        [HttpPatch("{branchId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int branchId, [FromBody] Dictionary<string, object> updates)
        {
            var updated = await _service.PatchAsync(branchId, updates);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove uma filial.
        /// </summary>
        /// <param name="branchId">ID da filial.</param>
        [HttpDelete("{branchId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int branchId)
        {
            var deleted = await _service.DeleteAsync(branchId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

