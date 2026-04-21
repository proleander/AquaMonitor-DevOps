using AquaMonitor.Api.Services;
using AquaMonitor.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaMonitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoAguaController : ControllerBase
    {
        private readonly IConsumoAguaService _service;

        public ConsumoAguaController(IConsumoAguaService service)
        {
            _service = service;
        }

        // ============================================================
        // GET COM PAGINAÇÃO
        // GET api/ConsumoAgua?page=1&pageSize=10
        // ============================================================
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _service.GetAllAsync(page, pageSize);
            return Ok(result);
        }

        // ============================================================
        // GET POR ID
        // GET api/ConsumoAgua/5
        // ============================================================
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new { message = "Registro não encontrado." });

            return Ok(result);
        }

        // ============================================================
        // POST – CRIAR REGISTRO
        // POST api/ConsumoAgua
        // ============================================================
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConsumoAguaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(model);

            // Melhor prática REST: retorna 201 com o caminho do recurso criado
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // ============================================================
        // PUT – ATUALIZAR REGISTRO
        // PUT api/ConsumoAgua/5
        // ============================================================
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateConsumoAguaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(id, model);

            if (result == null)
                return NotFound(new { message = "Registro não encontrado." });

            return Ok(result);
        }

        // ============================================================
        // DELETE – REMOVER REGISTRO
        // DELETE api/ConsumoAgua/5
        // ============================================================
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Registro não encontrado." });

            return Ok(new { message = "Registro removido com sucesso." });
        }
    }
}
