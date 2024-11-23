using Microsoft.AspNetCore.Mvc;
using Hackacont2024.Data;
using Hackacont2024.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackacont2024.Controllers {
    [Route("api/controller")]
    [ApiController]
    public class CargoController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public CargoController(ApplicationDbContext context) {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateCargo([FromBody] Cargo cargo) {
            if (string.IsNullOrEmpty(cargo.Category) || cargo.Quantity <= 0 || cargo.Weight <= 0) {
                return BadRequest("Categoria, quantidade e peso são obrigatórios!");
            }

            _context.Cargas.Add(cargo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCargo), new { id = cargo.Id }, cargo);

        }

        // Read (get all the cargos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetCargo() {
            return await _context.Cargas.ToListAsync();
        }

        // Read (get cargo by ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id) {
            var cargo = await _context.Cargas.FindAsync(id);

            if (cargo == null) {
                return NotFound();
            }

            return cargo;

        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCargo(int id, [FromBody] Cargo cargo) {
            if (id != cargo.Id) {
                return BadRequest("Carga inexistente!");
            }

            _context.Entry(cargo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id) {

            var cargo = await _context.Cargas.FindAsync(id);

            if (cargo == null) {
                return NotFound();
            }

            _context.Cargas.Remove(cargo);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}