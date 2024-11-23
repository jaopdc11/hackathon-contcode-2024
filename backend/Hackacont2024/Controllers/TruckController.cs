using Microsoft.AspNetCore.Mvc;
using Hackacont2024.Data;
using Hackacont2024.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackacont2024.Controllers {
    [Route("api/controller")]
    [ApiController]
    public class CaminhaoController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public CaminhaoController(ApplicationDbContext context) {
            _context = context;
        }

        // Create a new truck
        [HttpPost]
        public async Task<IActionResult> CreateCaminhao([FromBody] Caminhao caminhao) {
            if (string.IsNullOrEmpty(caminhao.Placa)) {
                return BadRequest("A placa do caminhão é obrigatória!");
            }

            // Validate if cargaId exists in the database
            var carga = await _context.Cargas.FindAsync(caminhao.CargaId);
            if (carga == null) {
                return BadRequest("A carga especificada não existe!");
            }

            _context.Caminhoes.Add(caminhao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCaminhao), new { id = caminhao.Id }, caminhao);
        }

        // Read all trucks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caminhao>>> GetCaminhoes() {
            // Include cargo information for each truck
            return await _context.Caminhoes
                .Include(c => c.Carga)
                .ToListAsync();
        }

        // Read a truck by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Caminhao>> GetCaminhao(int id) {
            var caminhao = await _context.Caminhoes
                .Include(c => c.Carga) // Include related cargo data
                .FirstOrDefaultAsync(c => c.Id == id);

            if (caminhao == null) {
                return NotFound();
            }

            return caminhao;
        }

        // Update a truck's details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCaminhao(int id, [FromBody] Caminhao caminhao) {
            if (id != caminhao.Id) {
                return BadRequest("O caminhão com o ID inserido não existe!");
            }

            // Validate if cargaId exists
            var carga = await _context.Cargas.FindAsync(caminhao.CargaId);
            if (carga == null) {
                return BadRequest("A carga especificada não existe!");
            }

            _context.Entry(caminhao).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!_context.Caminhoes.Any(c => c.Id == id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // Delete a truck by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaminhao(int id) {
            var caminhao = await _context.Caminhoes.FindAsync(id);
            if (caminhao == null) {
                return NotFound();
            }

            _context.Caminhoes.Remove(caminhao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
